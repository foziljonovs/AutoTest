using AutoTest.BLL.DTOs.Tests.Option;

namespace AutoTest.Desktop.Integrated.Servers.Interfaces.Option;

public interface IOptionServer
{
    Task<List<OptionDto>> GetAllAsync();
    Task<bool> AddAsync(CreateOptionDto dto);
}
