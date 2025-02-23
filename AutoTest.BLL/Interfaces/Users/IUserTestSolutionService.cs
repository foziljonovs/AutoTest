using AutoTest.BLL.DTOs.Users.UserTestSolution;

namespace AutoTest.BLL.Interfaces.Users;

public interface IUserTestSolutionService
{
    Task<IEnumerable<UserTestSolutionDto>> GetAllByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserTestSolutionDto>> GetAllByTestIdAsync(long testId, CancellationToken cancellationToken = default);
    Task<bool> AddAsync(CreateUserTestSolutionDto dto, CancellationToken cancellation = default);
    Task<bool> UpdateAsync(long id, UpdateUserTestSolutionDto dto, CancellationToken cancellation = default);
}
