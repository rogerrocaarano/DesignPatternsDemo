using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApi.Api.Endpoints;
using WebApi.Infrastructure.Data;
using static WebApi.Infrastructure.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Apply pending migrations (and seed data) at startup.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseCors(DevelopmentCorsPolicy);
}

app.UseHttpsRedirection();

app.MapCustomerEndpoints();
app.MapProductEndpoints();
app.MapSaleEndpoints();

app.Run();