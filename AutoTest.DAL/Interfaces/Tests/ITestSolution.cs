using AutoTest.Domain.Entities.Tests;

namespace AutoTest.DAL.Interfaces.Tests;

public interface ITestSolution : IRepository<TestSolution>
{
    Task<List<TestSolution>> GetAllFullInformationAsync();
    Task<TestSolution> GetSolutionAsync(long id);
}
