using AutoTest.BLL.DTOs.Users.UserTestSolution;

namespace AutoTest.Desktop.Integrated.Servers.Interfaces.User;

public interface IUserTestSolutionServer
{
    Task<List<UserTestSolutionDto>> GetAllByUserIdAsync(long userId);
    Task<List<UserTestSolutionDto>> GetAllByTestIdAsync(long testId);
    Task<long> AddAsync(CreateUserTestSolutionDto dto);
    Task<bool> UpdateAsync(long id, UpdateUserTestSolutionDto dto);
}
