using AutoTest.BLL.DTOs.Tests.Test;

namespace AutoTest.Desktop.Integrated.Services.Test;

public interface ITestService
{
    Task<List<TestDto>> GetAllAsync();
}
