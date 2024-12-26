using AutoMapper;
using AutoTest.BLL.Common.Exceptions;
using AutoTest.BLL.DTOs.Tests.Topic;
using AutoTest.BLL.Interfaces.Tests.Topic;
using AutoTest.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Ct = AutoTest.Domain.Entities.Tests;

namespace AutoTest.BLL.Services.Tests.Topic;

public class TopicService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : ITopicService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<bool> AddAsync(CreateTopicDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var existsTest = await _unitOfWork.Test.GetById(dto.TestId);
            if(existsTest is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Test not found");

            var topic = _mapper.Map<Ct.Topic>(dto);
            topic.CreatedDate = DateTime.UtcNow.AddHours(5);

            await _unitOfWork.Topic.Add(topic);
            return true;
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occured while created the topic. {ex}");
        }
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellation = default)
    {
        try
        {
            var existsTopic = await _unitOfWork.Topic.GetById(id);
            if (existsTopic is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Topic not found");

            await _unitOfWork.Topic.Delete(existsTopic);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occured while deleted the topic. {ex}");
        }
    }
    public async Task<IEnumerable<TopicDto>> GetAllAsync(CancellationToken cancellation = default)
    {
        try
        {
            var topics = await _unitOfWork.Topic.GetAll().ToListAsync(cancellation);

            if(!topics.Any())
                throw new StatusCodeException(HttpStatusCode.NotFound, "No topics found");

            return _mapper.Map<IEnumerable<TopicDto>>(topics);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occured while getting the topics. {ex}");
        }
    }

    public async Task<TopicDto> GetByIdAsync(long id, CancellationToken cancellation = default)
    {
        try
        {
            var topic = await _unitOfWork.Topic.GetById(id);
            if (topic is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Topic not found");

            return _mapper.Map<TopicDto>(topic);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occured while getting the topic. {ex}");
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateTopicDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var existsTest = await _unitOfWork.Test.GetById(dto.TestId);
            if (existsTest is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Test not found");

            var existsTopic = await _unitOfWork.Topic.GetById(id);
            if (existsTopic is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Topic not found");

            _mapper.Map(dto, existsTopic);
            existsTest.UpdatedDate = DateTime.UtcNow.AddHours(5);

            var result = await _unitOfWork.Topic.Update(existsTopic);
            return result;
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occured while updated the topic. {ex}");
        }
    }
}
