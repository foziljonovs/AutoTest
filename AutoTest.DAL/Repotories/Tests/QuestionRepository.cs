using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces.Tests;
using AutoTest.Domain.Entities.Tests;
using Microsoft.EntityFrameworkCore;

namespace AutoTest.DAL.Repotories.Tests;

public class QuestionRepository : Repository<Question>, IQuestion
{
    private readonly AppDbContext _appDbContext;
    private readonly DbSet<Question> _questions;
    public QuestionRepository(AppDbContext context) : base(context)
    {
        _appDbContext = context;
        _questions = context.Set<Question>();
    }

    public async Task<List<Question>> GetAllFullInformationAsync()
    {
        return await _questions
            .Include(x => x.Test)
            .Include(x => x.Options)
            .ToListAsync();
    }
}
