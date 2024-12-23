using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces.Tests;
using AutoTest.Domain.Entities.Tests;

namespace AutoTest.DAL.Repotories.Tests;

public class OptionRepository : Repository<Option>, IOption
{
    public OptionRepository(AppDbContext context) : base(context)
    {
    }
}
