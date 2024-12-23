using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces.Tests;
using AutoTest.Domain.Entities.Tests;

namespace AutoTest.DAL.Repotories.Tests;

public class TestRepository : Repository<Test>, ITest
{
    public TestRepository(AppDbContext context) : base(context)
    {
    }
}
