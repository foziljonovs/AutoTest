using AutoTest.Domain.Entities.Files;
using AutoTest.Domain.Entities.Tests;
using AutoTest.Domain.Enums;
using AutoTest.BLL.DTOs.User;
using System.Text.Json.Serialization;
using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.BLL.DTOs.Tests.Topic;

namespace AutoTest.BLL.DTOs.Tests.Test;

public class TestDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TestLevel Level { get; set; }
    public TestStatus Status { get; set; }
    public long UserId { get; set; }
    public UserDto User { get; set; }
    public List<QuestionDto> Question { get; set; }
    public List<TopicDto> Topics { get; set; }
    public List<TestFile> Files { get; set; } 
}
