using Microsoft.AspNetCore.Http;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Domain.Errors;
using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Contracts;
using PulseHub.SharedKernel.Enums;
using PulseHub.SharedKernel.Results;

namespace PulseHub.Application.Avatars.Commands.CreateAvatar;

public record CreateAvatarCommand(IFormFile file) : ICommand;

public class CreateAvatarCommandHandler : ICommandHandler<CreateAvatarCommand>
{
    private readonly IMediaCompressionService _compressionService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IAvatarRepository _avatarRepository;

    public CreateAvatarCommandHandler(IMediaCompressionService compressionService,
                                      IUnitOfWork unitOfWork,
                                      IUserRepository userRepository,
                                      ICurrentUserService currentUser,
                                      IAvatarRepository avatarRepository)
    {
        _compressionService = compressionService;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _currentUser = currentUser;
        _avatarRepository = avatarRepository;
    }

    public async Task<Result> Handle(CreateAvatarCommand request, CancellationToken cancellationToken)
    {
        // Getting the username and email of the user of the current session
        (string? username, string? email) = _currentUser.GetCurrentUser();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
        {
            return Result.Failure(Error.NotFound("The current user was not found!!"));
        }

        // Opening a stream from the upload file
        using Stream fileStream = request.file.OpenReadStream();

        // Compress and save the image
        Result<CompressionResult> compressionResult = _compressionService.ImageCompressionAndSave(
            fileStream,
            request.file.Length,
            Path.GetExtension(request.file.FileName),
            ImageQuality.Good);

        // If the compression process fail return the error
        if (compressionResult.IsFailure) return compressionResult;

        (string filename, string path, long totalSize, int height, int width, string format) = compressionResult.Value;

        // Getting the current avatar of the current user 
        Avatar? currentAvatar = await _avatarRepository.GetUserAvatarByUsernameAsync(username, cancellationToken);

        // Getting the information of the current user
        User? currentUser = await _userRepository.GetUserByUsernameAndEmailAsync(username, email);

        if (currentUser is null)
        {
            return Result.Failure(UserErrors.UserNotFoundByUsernameAndEmail(username, email));
        }

        if (currentAvatar is not null)
        {
            // Deleting the file if it exists
            if (File.Exists(currentAvatar.Path))
            {
                File.Delete(currentAvatar.Path);
            }

            // Marking as modified
            currentAvatar.Format = format;
            currentAvatar.FileName = filename;
            currentAvatar.Path = path;
            currentAvatar.TotalSize = totalSize;
            currentAvatar.Height = height;
            currentAvatar.Width = width;

            _avatarRepository.Update(currentAvatar);
        }
        else
        {
            //Creating a new avatar and marking it as add
            Avatar newAvatar = new Avatar
            {
                FileName = filename,
                Format = format,
                Height = height,
                Width = width,
                Path = path,
                TotalSize = totalSize,
                User = currentUser,
            };

            _unitOfWork.ChangeTrackerToUnchanged(newAvatar.User);

            _avatarRepository.Add(newAvatar);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
