using AutoMapper;
using AutoTest.BLL.Common.Exceptions;
using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.Interfaces.Tests.Test;
using AutoTest.DAL.Interfaces;
using AutoTest.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Net;
using CT = AutoTest.Domain.Entities.Tests;

namespace AutoTest.BLL.Services.Tests.Test;

public class TestService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : ITestService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<bool> AddAsync(CreateTestDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var existsUser = await _unitOfWork.User.GetById(dto.UserId);
            if(existsUser is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            var test = _mapper.Map<CT.Test>(dto);

            foreach(var topicId in dto.Topics)
            {
                var topic = await _unitOfWork.Topic.GetById(topicId);

                if (topic is not null)
                    test.Topics.Add(topic);
                else
                    break;
            }

            test.CreatedDate = DateTime.UtcNow.AddHours(5);
            await _unitOfWork.Test.Add(test);

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occured while created the test. {ex}");
        }
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellation = default)
    {
        try
        {
            var test = await _unitOfWork.Test.GetById(id);

            if(test is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Test not found");

            await _unitOfWork.Test.Delete(test);
            return true;
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occured while deleting the test. {ex}");
        }
    }

    public async Task<IEnumerable<TestDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var tests = await _unitOfWork.Test.GetAllFullInformationAsync();

            if(!tests.Any())
                throw new StatusCodeException(HttpStatusCode.NotFound, "No tests found");

            return _mapper.Map<IEnumerable<TestDto>>(tests);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occured while getting all tests. {ex}");
        }
    }

    public async Task<IEnumerable<TestDto>> GetAllByUserIdAsync(long userId, CancellationToken cancellation = default)
    {
        try
        {
            var tests = await _unitOfWork.Test.GetAllFullInformationAsync();

            if (!tests.Any())
                throw new StatusCodeException(HttpStatusCode.NotFound, "No tests found");

            var results = tests.Where(x => x.User.Id == userId).ToList();
            return _mapper.Map<IEnumerable<TestDto>>(results);
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occured while getting all tests by user id. {ex}");
        }
    }

    public async Task<IEnumerable<TestDto>> GetAllCompletedAsync(CancellationToken cancellation = default)
    {
        try
        {
            var tests = await _unitOfWork.Test.GetAll()
                .Where(x => x.Status == TestStatus.Completed)
                .ToListAsync(cancellation);

            if(!tests.Any())
                throw new StatusCodeException(HttpStatusCode.NotFound, "No completed tests found");

            return _mapper.Map<IEnumerable<TestDto>>(tests);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occured while getting all completed tests. {ex}");
        }
    }

    public async Task<TestDto> GetByIdAsync(long id, CancellationToken cancellation = default)
    {
        try
        {
            var test = await _unitOfWork.Test.GetById(id);

            if (test is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Test not found");

            return _mapper.Map<TestDto>(test);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occured while getting the test. {ex}");
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateTestDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var existsUser = await _unitOfWork.User.GetById(dto.UserId);
            if(existsUser is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            var existsTest = await _unitOfWork.Test.GetById(id);
            if(existsTest is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Test not found");

            _mapper.Map(dto, existsTest);
            existsTest.UpdatedDate = DateTime.UtcNow.AddHours(5);

            var result = await _unitOfWork.Test.Update(existsTest);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occured while updating the test. {ex}");
        }
    }
}
