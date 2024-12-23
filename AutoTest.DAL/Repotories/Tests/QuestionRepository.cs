using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces.Tests;
using AutoTest.Domain.Entities.Tests;

namespace AutoTest.DAL.Repotories.Tests;

public class QuestionRepository : Repository<Question>, IQuestion
{
    public QuestionRepository(AppDbContext context) : base(context)
    {
    }
}
