using AutoTest.BLL.DTOs.Tests.Option;

namespace AutoTest.Desktop.Integrated.Services.Option;

public interface IOptionService
{
    Task<long> AddAsync(CreateOptionDto dto);
}
