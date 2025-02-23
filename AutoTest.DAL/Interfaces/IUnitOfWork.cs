using AutoTest.DAL.Interfaces.Files;
using AutoTest.DAL.Interfaces.Tests;
using AutoTest.DAL.Interfaces.Users;

namespace AutoTest.DAL.Interfaces;

public interface IUnitOfWork
{
    public IUser User { get; set; }
    public ITest Test { get; set; }
    public ITopic Topic { get; set; }
    public IQuestion Question { get; set; }
    public IOption Option { get; set; }
    public ITestFile TestFile { get; set; }
    public IUserTestSolution UserTestSolution { get; set; }
    public ITestSolution TestSolution { get; set; }
    public ISavedTest SavedTest { get; set; }
    public IQuestionSolution QuestionSolution { get; set; }
}
