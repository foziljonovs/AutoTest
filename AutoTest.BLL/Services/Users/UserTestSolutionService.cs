using AutoMapper;
using AutoTest.BLL.Common.Exceptions;
using AutoTest.BLL.DTOs.Users.UserTestSolution;
using AutoTest.BLL.Interfaces.Users;
using AutoTest.DAL.Interfaces;
using AutoTest.Domain.Entities.Users;
using System.Net;

namespace AutoTest.BLL.Services.Users;

public class UserTestSolutionService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IUserTestSolutionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<bool> AddAsync(CreateUserTestSolutionDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var existsUser = await _unitOfWork.User.GetById(dto.UserId);
            if (existsUser is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, $"User with id {dto.UserId} not found");

            var existsTestSolution = await _unitOfWork.TestSolution.GetById(dto.TestSolutionId);
            if (existsTestSolution is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, $"Test solution with id {dto.TestSolutionId} not found");

            var userTestSolution = _mapper.Map<UserTestSolution>(dto);
            var result = await _unitOfWork.UserTestSolution.Add(userTestSolution);

            return result;
        }
        catch(Exception ex)
        {
            throw new Exception("An error occurred while adding the user test solution", ex);
        }
    }

    public async Task<IEnumerable<UserTestSolutionDto>> GetAllByTestIdAsync(long testId, CancellationToken cancellationToken = default)
    {
        try
        {
            var userTestSolutions = _unitOfWork.UserTestSolution.GetAll();
            if(!userTestSolutions.Any())
                throw new StatusCodeException(HttpStatusCode.NotFound, "No user test solutions found");

            return _mapper.Map<IEnumerable<UserTestSolutionDto>>(userTestSolutions);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting all user test solutions by test id", ex);
        }
    }

    public async Task<IEnumerable<UserTestSolutionDto>> GetAllByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var userTestSolutions = _unitOfWork.UserTestSolution.GetAll().Where(x => x.UserId == userId);
            if (!userTestSolutions.Any())
                throw new StatusCodeException(HttpStatusCode.NotFound, "No user test solutions found");

            return _mapper.Map<IEnumerable<UserTestSolutionDto>>(userTestSolutions);
        }
        catch(Exception ex)
        {
            throw new Exception("An error occurred while getting all user test solutions by user id", ex);
        }
    }

    public Task<bool> UpdateAsync(long id, UpdateUserTestSolutionDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var existsUser = _unitOfWork.User.GetById(dto.UserId);
            if (existsUser is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, $"User with id {dto.UserId} not found");

            var existsTestSolution = _unitOfWork.TestSolution.GetById(dto.TestSolutionId);
            if (existsTestSolution is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, $"Test solution with id {dto.TestSolutionId} not found");

            var userTestSolution = _mapper.Map<UserTestSolution>(dto);
            userTestSolution.Id = id;
            userTestSolution.UpdatedDate = DateTime.UtcNow.AddHours(5);

            var result = _unitOfWork.UserTestSolution.Update(userTestSolution);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the user test solution", ex);
        }
    }
}
