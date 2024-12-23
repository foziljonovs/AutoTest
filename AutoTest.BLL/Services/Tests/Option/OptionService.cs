using AutoMapper;
using AutoTest.BLL.Common.Exceptions;
using AutoTest.BLL.DTOs.Tests.Option;
using AutoTest.BLL.Interfaces.Tests.Option;
using AutoTest.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Ct = AutoTest.Domain.Entities.Tests;

namespace AutoTest.BLL.Services.Tests.Option;

public class OptionService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IOptionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<bool> AddAsync(CreateOptionDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var existsQuestion = await _unitOfWork.Question.GetById(dto.QuestionId);
            if(existsQuestion is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Question not found");

            var option = _mapper.Map<Ct.Option>(dto);
            option.CreatedDate = DateTime.Now.AddHours(5);

            await _unitOfWork.Option.Add(option);
            return true;
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occured while created the option. {ex}");
        }
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellation = default)
    {
        try
        {
            var option = await _unitOfWork.Option.GetById(id);
            if (option is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Option not found");

            var result = await _unitOfWork.Option.Delete(option);
            return result;
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occured while deleting the option. {ex}");
        }
    }

    public async Task<IEnumerable<OptionDto>> GetAllAsync(CancellationToken cancellation = default)
    {
        try
        {
            var options = await _unitOfWork.Option.GetAll().ToListAsync(cancellation);
            if(!options.Any())
                throw new StatusCodeException(HttpStatusCode.NotFound, "Options not found");

            return _mapper.Map<IEnumerable<OptionDto>>(options);
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occured while getting the options. {ex}");
        }
    }

    public async Task<OptionDto> GetByIdAsync(long id, CancellationToken cancellation = default)
    {
        try
        {
            var option = await _unitOfWork.Option.GetById(id);
            if (option is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Option not found");

            return _mapper.Map<OptionDto>(option);
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occured while getting the option. {ex}");
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateOptionDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var existsQuestion = await _unitOfWork.Question.GetById(dto.QuestionId);
            if(existsQuestion is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Question not found");

            var option = await _unitOfWork.Option.GetById(id);
            if (option is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Option not found");

            var updateOption = _mapper.Map<Ct.Option>(dto);
            updateOption.Id = id;
            updateOption.CreatedDate = option.CreatedDate.AddHours(5);

            var result = await _unitOfWork.Option.Update(updateOption);
            return result;
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occured while updating the option. {ex}");
        }
    }
}
