using AutoTest.Domain.Entities.Files;
using AutoTest.Domain.Entities.Tests;
using AutoTest.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace AutoTest.DAL.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<TestFile> TestFiles { get; set; }
}
