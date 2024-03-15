using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

//port 5201
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "OptibenchMonitor API", Description = "Testing OptibenchMonitor", Version = "v1" });
});

//za bazu
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<ResultsContext>(m => m.UseNpgsql(connectionString));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "OptibenchMonitor API V1");
});

app.MapGet("/results", async (ResultsContext db) => await db.Results.ToListAsync());
app.MapGet("/param/{id}", async (ResultsContext db, int id) =>
{
    var result = await db.Results.FindAsync(id);
    if (result == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(result.Params);
});
app.Run();
