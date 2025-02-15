using AutoTest.BLL.DTOs.Tests.Option;

namespace AutoTest.Desktop.Integrated.Services.Option;

public interface IOptionService
{
    Task<long> AddAsync(CreateOptionDto dto);
    Task<List<OptionDto>> GetAllAsync();
    Task<bool> UpdateAsync(long id, UpdateOptionDto dto);
    Task<bool> DeleteAsync(long id);
}
