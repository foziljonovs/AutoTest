using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces;
using AutoTest.DAL.Interfaces.Tests;
using AutoTest.Domain.Entities.Tests;

namespace AutoTest.DAL.Repotories.Tests;

public class QuestionSolutionRepository : Repository<QuestionSolution>, IQuestionSolution
{
    public QuestionSolutionRepository(AppDbContext context) : base(context)
    {
    }
}
