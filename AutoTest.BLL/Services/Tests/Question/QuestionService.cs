using AutoMapper;
using AutoTest.BLL.Common.Exceptions;
using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.BLL.Interfaces.Tests.Question;
using AutoTest.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;
using CT = AutoTest.Domain.Entities.Tests;

namespace AutoTest.BLL.Services.Tests.Question;

public class QuestionService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IQuestionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<bool> AddAsync(CreateQuestionDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var existsTest = await _unitOfWork.Test.GetById(dto.TestId);
            if(existsTest is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Test not found");

            var question = _mapper.Map<CT.Question>(dto);
            question.CreatedDate = DateTime.Now.AddHours(5);

            await _unitOfWork.Question.Add(question);
            return true;
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occured while created the question. {ex}");
        }
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellation = default)
    {
        try
        {
            var question = await _unitOfWork.Question.GetById(id);

            if (question == null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Question not found");

            await _unitOfWork.Question.Delete(question);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occured while deleting the question. {ex}");
        }
    }
    public Task<IEnumerable<QuestionDto>> GetAllAsync(CancellationToken cancellation = default)
    {
        try
        {
            var questions = _unitOfWork.Question.GetAll();

            if(!questions.Any())
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "No questions found");
            }

            var questionsDto = _mapper.Map<IEnumerable<QuestionDto>>(questions);
            return Task.FromResult(questionsDto);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occured while getting all questions. {ex}");
        }
    }

    public async Task<QuestionDto> GetByIdAsync(long id, CancellationToken cancellation = default)
    {
        try
        {
            var question = await _unitOfWork.Question.GetById(id);
            if(question is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Question not found");

            return _mapper.Map<QuestionDto>(question);
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occured while getting the question. {ex}");
        }
    }

    public async Task<IEnumerable<QuestionDto>> GetQuestionsByTestAsync(long testId, CancellationToken cancellation = default)
    {
        try
        {
            var existsTest = await _unitOfWork.Test.GetById(testId);
            if (existsTest is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Test not found");

            var questions = await _unitOfWork.Question.GetAll()
                .Where(x => x.TestId == testId)
                .ToListAsync(cancellation);

            if(!questions.Any())
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "No questions found");
            }

            return _mapper.Map<IEnumerable<QuestionDto>>(questions);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occured while getting the questions. {ex}");
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateQuestionDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var existsTest = await _unitOfWork.Test.GetById(dto.TestId);
            if (existsTest is null)                
                throw new StatusCodeException(HttpStatusCode.NotFound, "Test not found");

            var question = await _unitOfWork.Question.GetById(id);
            if (question is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Question not found");

            var updatedQuestion = _mapper.Map<CT.Question>(dto);
            updatedQuestion.Id = id;
            updatedQuestion.UpdatedDate = DateTime.Now.AddHours(5);

            var result = await _unitOfWork.Question.Update(updatedQuestion);
            return result;
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occured while updating the question. {ex}");
        }
    }
}
