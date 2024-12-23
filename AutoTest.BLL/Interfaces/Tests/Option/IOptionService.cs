using AutoTest.BLL.DTOs.Tests.Option;

namespace AutoTest.BLL.Interfaces.Tests.Option;

public interface IOptionService
{
    Task<IEnumerable<OptionDto>> GetAllAsync(CancellationToken cancellation = default);
    Task<OptionDto> GetByIdAsync(long id, CancellationToken cancellation = default);
    Task<bool> AddAsync(CreateOptionDto dto, CancellationToken cancellation = default);
    Task<bool> UpdateAsync(long id, UpdateOptionDto dto, CancellationToken cancellation = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellation = default);
}
