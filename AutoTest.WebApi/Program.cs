using AutoTest.BLL.Common.Mapper;
using AutoTest.WebApi.Configurations;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddDbConfigure(builder.Configuration)
    .AddServiceConfigure()
    .AddAutoMapper(typeof(MapperProfile))
    .AddEndpointsApiExplorer()
    .AddCorsConfigure()
    .AddSwaggerConfigure()
    .AddJwtConfigure(builder.Configuration)
    .AddOpenApi();
    
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
