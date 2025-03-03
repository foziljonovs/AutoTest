using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces.Tests;
using AutoTest.Domain.Entities.Tests;
using Microsoft.EntityFrameworkCore;

namespace AutoTest.DAL.Repotories.Tests;

public class TestSolutionRepository : Repository<TestSolution>, ITestSolution
{
    private readonly AppDbContext _dbContext;
    private DbSet<TestSolution> _solutions;
    public TestSolutionRepository(AppDbContext context) : base(context)
    {
        this._dbContext = context;
        this._solutions = context.Set<TestSolution>();
    }

    public async Task<List<TestSolution>> GetAllFullInformationAsync()
    {
        return await _dbContext.TestSolutions
            .Include(x => x.Test)
            .Include(x => x.QuestionSolutions)
            .ToListAsync();
    }

    public async Task<TestSolution> GetSolutionAsync(long id)
    {
        return await _dbContext.TestSolutions
            .Include(x => x.Test)
            .Include(x => x.QuestionSolutions)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
