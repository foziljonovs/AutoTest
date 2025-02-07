using AutoTest.BLL.Common.Mapper;
using AutoTest.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddHttpClient();

builder.Services
    .AddDbConfigure(builder.Configuration)
    .AddServiceConfigure()
    .AddAutoMapper(typeof(MapperProfile))
    .AddEndpointsApiExplorer()
    .AddCorsConfigure()
    .AddSwaggerConfigure()
    .AddJwtConfigure(builder.Configuration);

builder.Services.AddDeepSeek(builder.Configuration);
    
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
