using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces.Users;
using AutoTest.Domain.Entities.Users;

namespace AutoTest.DAL.Repotories.Users;

public class SavedTestRepository : Repository<SavedTest>, ISavedTest
{
    public SavedTestRepository(AppDbContext context) : base(context)
    {
    }
}
