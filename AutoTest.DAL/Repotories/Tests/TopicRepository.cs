using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces.Tests;
using AutoTest.Domain.Entities.Tests;

namespace AutoTest.DAL.Repotories.Tests;

public class TopicRepository : Repository<Topic>, ITopic
{
    public TopicRepository(AppDbContext context) : base(context)
    {
    }
}
