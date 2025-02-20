using AutoTest.BLL.Interfaces.Auth;
using AutoTest.BLL.Interfaces.Tests.Option;
using AutoTest.BLL.Interfaces.Tests.Question;
using AutoTest.BLL.Interfaces.Tests.Test;
using AutoTest.BLL.Interfaces.Tests.Topic;
using AutoTest.BLL.Services.Auth;
using AutoTest.BLL.Services.Tests.Option;
using AutoTest.BLL.Services.Tests.Question;
using AutoTest.BLL.Services.Tests.Test;
using AutoTest.BLL.Services.Tests.Topic;
using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces;
using AutoTest.DAL.Repotories;
using Microsoft.EntityFrameworkCore;

namespace AutoTest.WebApi.Configurations.Service;

public static class ServiceConfiguration
{
    public static IServiceCollection AddDbContextConfigure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("localhost");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }

    public static IServiceCollection AddServiceConfigure(
        this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITestService, TestService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IOptionService, OptionService>();
        services.AddScoped<ITopicService, TopicService>();

        return services;
    }
}
