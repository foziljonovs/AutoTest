﻿using AutoTest.BLL.DTOs.Tests.QuestionSolution;

namespace AutoTest.Desktop.Integrated.Services.Question;

public interface IQuestionSolutionService
{
    Task<List<QuestionSolutionDto>> GetAllAsync();
    Task<QuestionSolutionDto> GetByIdAsync(long id);
    Task<bool> AddAsync(CreateQuestionSolutionDto dto);
    Task<bool> UpdateAsync(long id, UpdateQuestionSolutionDto dto);
    Task<bool> DeleteAsync(long id);
    Task<List<QuestionSolutionDto>> GetAllByTestSolutionIdAsync(long testSolutionId);
}
