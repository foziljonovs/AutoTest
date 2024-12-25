using AutoTest.BLL.Common.Mapper;
using AutoTest.DAL.Data;
using AutoTest.WebApi.Configurations.Auth;
using AutoTest.WebApi.Configurations.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddDbContextConfigure(builder.Configuration)
    .AddServiceConfigure()
    .AddConfigureCors()
    .AddSwaggerConfigure()
    .AddJwtConfigure(builder.Configuration)
    .AddAutoMapper(typeof(MapperProfile));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoTest.WebApi v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
