﻿using AutoMapper;
using AutoTest.BLL.Common.Exceptions;
using AutoTest.BLL.DTOs.Tests.TestSolution;
using AutoTest.BLL.Interfaces.Tests.Test;
using AutoTest.DAL.Interfaces;
using AutoTest.Domain.Entities.Tests;
using System.Net;

namespace AutoTest.BLL.Services.Tests.Test;

public class TestSolutionService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : ITestSolutionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<bool> AddAsync(CreateTestSolutionDto createTestSolutionDto, CancellationToken cancellation = default)
    {
        try
        {
            var existsTest = await _unitOfWork.Test.GetById(createTestSolutionDto.TestId);
            if(existsTest is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Test not found");

            var testSolution = _mapper.Map<TestSolution>(createTestSolutionDto);
            testSolution.CreatedDate = DateTime.UtcNow.AddHours(5);
            testSolution.StartedAt = DateTime.UtcNow.AddHours(5);

            var result = await _unitOfWork.TestSolution.Add(testSolution);
            return result;
        }
        catch(Exception ex)
        {
            throw new Exception("An error occured while adding the test solution", ex);
        }
    }

    public async Task<long> AddTestSolutionAsync(CreateTestSolutionDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var existsTest = await _unitOfWork.Test.GetById(dto.TestId);
            if (existsTest is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, $"Test solution with id {dto.TestId} not found");

            var testSolution = _mapper.Map<TestSolution>(dto);
            testSolution.CreatedDate = DateTime.UtcNow.AddHours(5);
            testSolution.StartedAt = DateTime.UtcNow.AddHours(5);

            var result = await _unitOfWork.TestSolution.AddAsync(testSolution);
            return result;
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occured while adding the test solution", ex);
        }
    }

    public async Task<IEnumerable<TestSolutionDto>> GetAllAsync(CancellationToken cancellation = default)
    {
        try
        {
            var testSolutions = await _unitOfWork.TestSolution.GetAllFullInformationAsync();
            if(!testSolutions.Any())
                throw new StatusCodeException(HttpStatusCode.NotFound, "No test solutions found");

            return _mapper.Map<IEnumerable<TestSolutionDto>>(testSolutions);
        }   
        catch(Exception ex)
        {
            throw new Exception("An error occured while getting all test solutions", ex);
        }
    }

    public async Task<TestSolutionDto> GetByIdAsync(long id, CancellationToken cancellation = default)
    {
        try
        {
            var testSolution = await _unitOfWork.TestSolution.GetSolutionAsync(id);
            if (testSolution is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Test solution not found");

            return _mapper.Map<TestSolutionDto>(testSolution);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occured while getting the test solution", ex);
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateTestSolutionDto updateTestSolutionDto, CancellationToken cancellation = default)
    {
        try
        {
            var existsTestSolution = await _unitOfWork.TestSolution.GetById(id);
            if (existsTestSolution is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Test solution not found");

            var existsTest = await _unitOfWork.Test.GetById(updateTestSolutionDto.TestId);
            if (existsTest is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Test not found");

            _mapper.Map(updateTestSolutionDto, existsTestSolution);
            existsTestSolution.Id = id;
            existsTestSolution.UpdatedDate = DateTime.UtcNow.AddHours(5);
            existsTestSolution.FinishedAt = DateTime.UtcNow.AddHours(5);

            var result = await _unitOfWork.TestSolution.Update(existsTestSolution);
            return result;
        }
        catch(Exception ex)
        {
            throw new Exception("An error occured while updating the test solution", ex);
        }
    }
}
