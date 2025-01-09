using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoTest.BLL.DTOs.Tests.Test;
using System.Text.Json.Serialization;

namespace AutoTest.BLL.DTOs.Tests.Topic;

public class TopicDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}
