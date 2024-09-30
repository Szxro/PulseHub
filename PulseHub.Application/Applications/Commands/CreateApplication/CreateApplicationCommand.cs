using PulseHub.Application.Common.DTOs.Responses;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Domain.Errors;
using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Contracts;
using DomainApplication = PulseHub.Domain.Entities.Application;

namespace PulseHub.Application.Applications.Commands.CreateApplication;

public record CreateApplicationCommand(
    string name,
    string description,
    string provider,
    string providerApplicationId) : ICommand<ApplicationCreatedResponse>;

public class CreateApplicationCommandHandler : ICommandHandler<CreateApplicationCommand, ApplicationCreatedResponse>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProviderRepository _providerRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IAccessKeyService _accessKeyService;

    public CreateApplicationCommandHandler(
        IApplicationRepository applicationRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IProviderRepository providerRepository,
        ICurrentUserService currentUserService,
        IAccessKeyService accessKeyService)
    {
        _applicationRepository = applicationRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _providerRepository = providerRepository;
        _currentUserService = currentUserService;
        _accessKeyService = accessKeyService;
    }

    public async Task<Result<ApplicationCreatedResponse>> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        if (await _applicationRepository.IsApplicationNameNotUnique(request.name))
        {
            return Result<ApplicationCreatedResponse>.Failure(ApplicationErrors.ApplicationNameNotUnique(request.name));
        }

        (string? username,string? email) = _currentUserService.GetCurrentUser();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
        {
            return Result<ApplicationCreatedResponse>.Failure(Error.NotFound("The current user was not found!!"));
        }

        User? foundUser = await _userRepository.GetUserByUsernameAndEmailAsync(username, email);

        if (foundUser is null)
        {
            return Result<ApplicationCreatedResponse>.Failure(UserErrors.UserNotFoundByUsernameAndEmail(username, email));
        }

        Provider? foundProvider = await _providerRepository.GetProviderByNameAsync(request.provider);

        if (foundProvider is null)
        {
            return Result<ApplicationCreatedResponse>.Failure(ProviderErrors.ProviderNotFoundByName(request.provider));
        }

        DomainApplication newApplication = new DomainApplication
        {
            Name = request.name,
            Description = request.description,
            User = foundUser,
            Provider = foundProvider,
            ProviderApplicationId = request.providerApplicationId,
        };

        string newAccessKey = _accessKeyService.GenerateUniqueAccessKey();

        newApplication.AccessKeys.Add(new AccessKey()
        {
            EncryptedKey = _accessKeyService.EncryptAccessKey(newAccessKey),
            IsActive = true,
        });

        _unitOfWork.ChangeTrackerToUnchanged(newApplication.User);

        _unitOfWork.ChangeTrackerToUnchanged(newApplication.Provider);

        _applicationRepository.Add(newApplication);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ApplicationCreatedResponse>.Success(new ApplicationCreatedResponse(request.name, newAccessKey));
    }
}
