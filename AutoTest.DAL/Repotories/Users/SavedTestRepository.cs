using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces.Users;
using AutoTest.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace AutoTest.DAL.Repotories.Users;

public class SavedTestRepository : Repository<SavedTest>, ISavedTest
{
    private readonly AppDbContext _appDbContext;
    private DbSet<SavedTest> _savedTests;
    public SavedTestRepository(AppDbContext context) : base(context)
    {
        _appDbContext = context;
        _savedTests = context.Set<SavedTest>();
    }

    public async Task<List<SavedTest>> GetAllFullInformationAsync()
    {
        return await _savedTests
            .Include(x => x.User)
            .Include(x => x.Test)
            .ToListAsync();
    }
}
