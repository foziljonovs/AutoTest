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
using AutoTest.DAL.Interfaces.Tests;
using AutoTest.DAL.Repotories;
using Microsoft.EntityFrameworkCore;

namespace AutoTest.WebApi.Configurations;

public static class LayerConfiguration
{
    public static IServiceCollection AddDbConfigure(
        this IServiceCollection services, IConfiguration configuration)
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

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<ITestService, TestService>();
        services.AddScoped<ITopicService, TopicService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IOptionService, OptionService>();

        return services;
    }
}
