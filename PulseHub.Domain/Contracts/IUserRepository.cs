﻿using PulseHub.Domain.Entities;
using PulseHub.Domain.ValueObjects;

namespace PulseHub.Domain.Contracts;

public interface IUserRepository : IRepositoryWriter<User>
{
    Task<bool> IsEmailUnique(Email email);

    Task<bool> IsUserNameUnique(string username);

    Task<User?> GetUserByUsernameAndEmailAsync(string username, string email, CancellationToken cancellationToken = default);

    Task<User?> GetUserByUsernameAsync(string username,CancellationToken cancellationToken = default);
}
