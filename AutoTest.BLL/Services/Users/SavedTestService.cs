using AutoMapper;
using AutoTest.BLL.Common.Exceptions;
using AutoTest.BLL.DTOs.Users.SavedTest;
using AutoTest.BLL.Interfaces.Users;
using AutoTest.DAL.Interfaces;
using AutoTest.Domain.Entities.Users;
using System.Net;

namespace AutoTest.BLL.Services.Users;

public class SavedTestService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : ISavedTestService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<bool> AddAsync(CreatedSavedTestDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var userExists = await _unitOfWork.User.GetById(dto.UserId);

            if (userExists is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            var savedTest = _mapper.Map<SavedTest>(dto);
            savedTest.CreatedDate = DateTime.UtcNow.AddHours(5);

            var result = await _unitOfWork.SavedTest.Add(savedTest);
            return result;
        }
        catch(Exception ex)
        {
            throw new Exception("An error occurred while adding the saved test", ex);
        }
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellation = default)
    {
        try
        {
            var savedTest = await _unitOfWork.SavedTest.GetById(id);
            if(savedTest is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Saved test not found");

            var result = await _unitOfWork.SavedTest.Delete(savedTest);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the saved test", ex);
        }
    }

    public async Task<IEnumerable<SavedTestDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var savedTests = _unitOfWork.SavedTest.GetAll();
            if(!savedTests.Any())
                throw new StatusCodeException(HttpStatusCode.NotFound, "No saved tests found");

            return _mapper.Map<IEnumerable<SavedTestDto>>(savedTests);
        }
        catch(Exception ex)
        {
            throw new Exception("An error occurred while getting all saved tests", ex);
        }
    }

    public async Task<IEnumerable<SavedTestDto>> GetAllByTestIdAsync(long testId, CancellationToken cancellationToken = default)
    {
        try
        {
            var savedTests = _unitOfWork.SavedTest.GetAll().Where(x => x.TestId == testId);
            if(!savedTests.Any())
                throw new StatusCodeException(HttpStatusCode.NotFound, "No saved tests found");

            return _mapper.Map<IEnumerable<SavedTestDto>>(savedTests);
        }
        catch(Exception ex)
        {
            throw new Exception("An error occurred while getting all saved tests by test id", ex);
        }
    }

    public async Task<IEnumerable<SavedTestDto>> GetAllByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var savedTests = _unitOfWork.SavedTest.GetAll().Where(x => x.UserId == userId);
            if(!savedTests.Any())
                throw new StatusCodeException(HttpStatusCode.NotFound, "No saved tests found");

            return _mapper.Map<IEnumerable<SavedTestDto>>(savedTests);
        }
        catch(Exception ex)
        {
            throw new Exception("An error occurred while getting all saved tests by user id", ex);
        }
    }

    public async Task<SavedTestDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        try
        {
            var savedTest = await _unitOfWork.SavedTest.GetById(id);
            if(savedTest is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Saved test not found");

            return _mapper.Map<SavedTestDto>(savedTest);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting saved test by id", ex);
        }
    }
}
