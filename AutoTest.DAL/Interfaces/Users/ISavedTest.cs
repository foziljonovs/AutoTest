using AutoTest.Domain.Entities.Users;

namespace AutoTest.DAL.Interfaces.Users;

public interface ISavedTest : IRepository<SavedTest>
{
    Task<List<SavedTest>> GetAllFullInformationAsync();
}
