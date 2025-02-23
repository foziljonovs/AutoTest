using AutoTest.BLL.DTOs.Users.UserTestSolution;
using AutoTest.BLL.Interfaces.Users;

namespace AutoTest.BLL.Services.Users;

public class UserTestSolutionService : IUserTestSolutionService
{
    public Task<bool> AddAsync(CreateUserTestSolutionDto dto, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserTestSolutionDto>> GetAllByTestIdAsync(long testId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserTestSolutionDto>> GetAllByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(long id, UpdateUserTestSolutionDto dto, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }
}
