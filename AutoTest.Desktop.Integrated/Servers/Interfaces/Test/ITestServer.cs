using AutoTest.BLL.DTOs.Tests.Test;

namespace AutoTest.Desktop.Integrated.Servers.Interfaces.Test;

public interface ITestServer
{
    Task<List<TestDto>> GetAllAsync();
}
