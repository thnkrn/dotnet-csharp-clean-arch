using DotnetCleanArch.API.Middleware;
using DotnetCleanArch.Application.interfaces;
using DotnetCleanArch.Application.Queries;
using DotnetCleanArch.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IProductQueries, ProductQueries>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandler>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();