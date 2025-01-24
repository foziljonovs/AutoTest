using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces.Tests;
using AutoTest.Domain.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal;

namespace AutoTest.DAL.Repotories.Tests;

public class TestRepository : Repository<Test>, ITest
{
    private readonly AppDbContext _appDbContext;
    private DbSet<Test> _tests;
    public TestRepository(AppDbContext context) : base(context)
    {
        _appDbContext = context;
        _tests = context.Set<Test>();
    }

    public async Task<List<Test>> GetAllFullInformationAsync()
    {
        return await _tests
            .Include(x => x.User)
            .Include(x => x.Topics)
            .Include(x => x.Question)
                .ThenInclude(x => x.Options)
            .ToListAsync();
    }

    public async Task<Test> GetByIdAsync(long id)
    {
        return await _tests
            .Include(x => x.User)
            .Include(x => x.Topics)
            .Include(x => x.Question)
                .ThenInclude(x => x.Options)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
