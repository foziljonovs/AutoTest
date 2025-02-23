using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces.Tests;
using AutoTest.Domain.Entities.Tests;

namespace AutoTest.DAL.Repotories.Tests;

public class TestSolutionRepository : Repository<TestSolution>, ITestSolution
{
    public TestSolutionRepository(AppDbContext context) : base(context)
    {
    }
}
