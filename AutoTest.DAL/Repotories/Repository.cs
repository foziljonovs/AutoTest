using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces;
using AutoTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoTest.DAL.Repotories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private DbSet<TEntity> _dbSet;
    private AppDbContext _context;
    public Repository(AppDbContext context)
    {
        _dbSet = context.Set<TEntity>();
        _context = context;
    }
    public async Task<bool> Add(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        int res = await _context.SaveChangesAsync();

        return res > 0;
    }

    public async Task<long> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> AddRange(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        int res = await _context.SaveChangesAsync();

        return res > 0;
    }

    public async Task<bool> Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
        int res = await _context.SaveChangesAsync();

        return res > 0;
    }

    public IQueryable<TEntity> GetAll()
        => _dbSet.AsQueryable();

    public async Task<TEntity?> GetById(long id)
    {
        TEntity? entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return null;

        return entity;
    }

    public async Task<bool> Update(TEntity entity)
    {
        _dbSet.Update(entity);
        int res = await _context.SaveChangesAsync();

        return res > 0;
    }

    public async Task<bool> UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
        int res = await _context.SaveChangesAsync();

        return res > 0;
    }
}
