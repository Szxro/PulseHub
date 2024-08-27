﻿using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Domain.Contracts;

public interface IRepositoryWriter<TEntity>
    where TEntity : IEntity
{
    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entities);
}
