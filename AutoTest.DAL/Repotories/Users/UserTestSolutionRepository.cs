using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces.Users;
using AutoTest.Domain.Entities.Users;

namespace AutoTest.DAL.Repotories.Users;

public class UserTestSolutionRepository : Repository<UserTestSolution>, IUserTestSolution
{
    public UserTestSolutionRepository(AppDbContext context) : base(context)
    {
    }
}
