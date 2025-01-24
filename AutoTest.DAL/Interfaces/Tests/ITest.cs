using AutoTest.Domain.Entities.Tests;

namespace AutoTest.DAL.Interfaces.Tests;

public interface ITest : IRepository<Test>
{
    Task<List<Test>> GetAllFullInformationAsync();
    Task<Test> GetByIdAsync(long id);
}
