﻿using AutoTest.Domain.Entities;

namespace AutoTest.DAL.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<bool> Add(TEntity entity);
    Task<long> AddAsync(TEntity entity);
    Task<bool> AddRange(IEnumerable<TEntity> entities);
    Task<bool> Update(TEntity entity);
    Task<bool> UpdateRange(IEnumerable<TEntity> entities);
    Task<bool> Delete(TEntity entity);
    Task<TEntity?> GetById(long id);
    IQueryable<TEntity> GetAll();
}
