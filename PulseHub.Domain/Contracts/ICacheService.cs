﻿using Microsoft.Extensions.Caching.Memory;

namespace PulseHub.Domain.Contracts;

public interface ICacheService
{
    void Set<T>(string key, T value, MemoryCacheEntryOptions? options = null);

    T? Get<T>(string key);

    void Remove(string key);
}
