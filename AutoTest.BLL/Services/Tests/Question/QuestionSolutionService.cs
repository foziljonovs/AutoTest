using AutoMapper;
using AutoTest.BLL.Common.Exceptions;
using AutoTest.BLL.DTOs.Tests.QuestionSolution;
using AutoTest.BLL.Interfaces.Tests.Question;
using AutoTest.DAL.Interfaces;
using AutoTest.Domain.Entities.Tests;

namespace AutoTest.BLL.Services.Tests.Question;

public class QuestionSolutionService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IQuestionSolutionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<bool> AddAsync(CreateQuestionSolutionDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var existsQuestion = await _unitOfWork.QuestionSolution.GetById(dto.QuestionId);
            if(existsQuestion is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Question not found");

            var existsTestSolution = await _unitOfWork.TestSolution.GetById(dto.TestSolutionId);
            if (existsTestSolution is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Test solution not found");

            var questionSolution = _mapper.Map<QuestionSolution>(dto);
            questionSolution.CreatedDate = DateTime.UtcNow.AddHours(5);

            var result = await _unitOfWork.QuestionSolution.Add(questionSolution);
            return result;
        }
        catch(Exception ex)
        {
            throw new Exception("An error occured while adding the question solution", ex); 
        }
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        try
        {
            var exists = await _unitOfWork.QuestionSolution.GetById(id);
            if (exists is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Question solution not found");

            var result = await _unitOfWork.QuestionSolution.Delete(exists);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occured while deleting the question solution", ex);
        }
    }

    public async Task<IEnumerable<QuestionSolutionDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var questionSolutions = _unitOfWork.QuestionSolution.GetAll();
            if(!questionSolutions.Any())
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "No question solutions found");

            return _mapper.Map<IEnumerable<QuestionSolutionDto>>(questionSolutions);
        }
        catch(Exception ex)
        {
            throw new Exception("An error occured while getting all question solutions", ex);
        }
    }

    public async Task<IEnumerable<QuestionSolutionDto>> GetAllByTestSolutionIdAsync(long testSolutionId, CancellationToken cancellationToken = default)
    {
        try
        {
            var questionSolutions = _unitOfWork.QuestionSolution.GetAll().Where(x => x.TestSolutionId == testSolutionId);
            if(!questionSolutions.Any())
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "No question solutions found");

            return _mapper.Map<IEnumerable<QuestionSolutionDto>>(questionSolutions);
        }
        catch(Exception ex)
        {
            throw new Exception("An error occured while getting all question solutions by test solution id", ex);
        }
    }

    public async Task<QuestionSolutionDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        try
        {
            var questionSolution = await _unitOfWork.QuestionSolution.GetById(id);
            if (questionSolution is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Question solution not found");

            return _mapper.Map<QuestionSolutionDto>(questionSolution);
        } 
        catch(Exception ex)
        {
            throw new Exception("An error occured while getting question solution by id", ex);
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateQuestionSolutionDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var exists = await _unitOfWork.QuestionSolution.GetById(id);
            if (exists is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Question solution not found");

            var existsQuestion = await _unitOfWork.QuestionSolution.GetById(dto.QuestionId);
            if (existsQuestion is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Question not found");

            var existsTestSolution = await _unitOfWork.TestSolution.GetById(dto.TestSolutionId);
            if (existsTestSolution is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Test solution not found");

            var questionSolution = _mapper.Map<QuestionSolution>(dto);
            questionSolution.Id = id;
            questionSolution.UpdatedDate = DateTime.UtcNow.AddHours(5);

            var result = await _unitOfWork.QuestionSolution.Update(questionSolution);
            return result;
        }
        catch(Exception ex)
        {
            throw new Exception("An error occured while updating the question solution", ex);
        }
    }
}
