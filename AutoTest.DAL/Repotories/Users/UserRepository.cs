using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces.Users;
using AutoTest.Domain.Entities.Users;

namespace AutoTest.DAL.Repotories.Users;

public class UserRepository : Repository<User>, IUser
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}
