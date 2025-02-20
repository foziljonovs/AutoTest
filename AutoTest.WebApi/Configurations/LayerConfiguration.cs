using AutoTest.BLL.Interfaces.Auth;
using AutoTest.BLL.Interfaces.OpenAI;
using AutoTest.BLL.Interfaces.Tests.Option;
using AutoTest.BLL.Interfaces.Tests.Question;
using AutoTest.BLL.Interfaces.Tests.Test;
using AutoTest.BLL.Interfaces.Tests.Topic;
using AutoTest.BLL.Services.Auth;
using AutoTest.BLL.Services.OpenAI;
using AutoTest.BLL.Services.Tests.Option;
using AutoTest.BLL.Services.Tests.Question;
using AutoTest.BLL.Services.Tests.Test;
using AutoTest.BLL.Services.Tests.Topic;
using AutoTest.DAL.Data;
using AutoTest.DAL.Interfaces;
using AutoTest.DAL.Repotories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;

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

        services.AddScoped<IOpenAIService, OpenAIService>();

        return services;
    }

    public static IServiceCollection AddAIConfigure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var model = configuration["Ollama:Model"];
        var host = configuration["Ollama:localhost"];

        IChatClient client = new OllamaChatClient(host, modelId: model)
            .AsBuilder()
            .UseFunctionInvocation()
            .Build();

        services.AddSingleton(client);

        return services;
    }
}
