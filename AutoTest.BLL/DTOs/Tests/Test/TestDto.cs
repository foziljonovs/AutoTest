using AutoTest.BLL.DTOs.User;
using AutoTest.Domain.Entities.Files;
using AutoTest.Domain.Enums;
using Et = AutoTest.Domain.Entities.Tests;

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
    public List<Et.Question> Question { get; set; }
    public List<Et.Topic> Topics { get; set; }
    public List<TestFile> Files { get; set; }
}
