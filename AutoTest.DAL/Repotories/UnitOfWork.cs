using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces;
using AutoTest.DAL.Interfaces.Files;
using AutoTest.DAL.Interfaces.Tests;
using AutoTest.DAL.Interfaces.Users;
using AutoTest.DAL.Repotories.Files;
using AutoTest.DAL.Repotories.Tests;
using AutoTest.DAL.Repotories.Users;

namespace AutoTest.DAL.Repotories;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork, IDisposable
{
    public IUser User { get; set; } = new UserRepository(dbContext);
    public ITest Test { get; set; } = new TestRepository(dbContext);
    public ITopic Topic { get; set; } = new TopicRepository(dbContext);
    public IQuestion Question { get; set; } = new QuestionRepository(dbContext);
    public IOption Option { get; set; } = new OptionRepository(dbContext);
    public ITestFile TestFile { get; set; } = new TestFileRepository(dbContext);
    public IUserTestSolution UserTestSolution { get; set; } = new UserTestSolutionRepository(dbContext);
    public ITestSolution TestSolution { get; set; } = new TestSolutionRepository(dbContext);
    public ISavedTest SavedTest { get; set; } = new SavedTestRepository(dbContext);
    public IQuestionSolution QuestionSolution { get; set; } = new QuestionSolutionRepository(dbContext);

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
