using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces.Files;
using AutoTest.Domain.Entities.Files;

namespace AutoTest.DAL.Repotories.Files;

public class TestFileRepository : Repository<TestFile>, ITestFile
{
    public TestFileRepository(AppDbContext context) : base(context)
    {
    }
}
