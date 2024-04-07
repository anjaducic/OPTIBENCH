using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Model;
using Newtonsoft.Json.Linq;

//port 5201
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => //dodato da bi radilo gadjanje sa fronta
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "OptibenchMonitor API", Description = "Testing OptibenchMonitor", Version = "v1" });
});

//za bazu
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<ResultsContext>(m => m.UseNpgsql(connectionString));

var app = builder.Build();
app.UseCors("AllowAnyOrigin");  //dodato da bi radilo gadjanje sa fronta




app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "OptibenchMonitor API V1");
});

app.MapGet("/results", async (ResultsContext db) => await db.Results.ToListAsync());    //get all
/*app.MapGet("/param/{id}", async (ResultsContext db, int id) =>
{
    var result = await db.Results.FindAsync(id);
    if (result == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(result.Params);
});*/
app.MapGet("/results/problemName/{problemName}/optimizerName/{optimizerName}", async (ResultsContext db, string problemName, string optimizerName) =>
{
    var allResults = await db.Results.ToListAsync();

    var filteredResults = allResults
            .Where(r => JObject.Parse(r.ProblemInfo)["ProblemName"]!.ToString() == problemName && r.OptimizerName == optimizerName)
            .ToList();
    return filteredResults;

});

app.MapPost("/result", async (ResultsContext db, OptimizationResult result) =>
{
    await db.Results.AddAsync(result);
    await db.SaveChangesAsync();
    return Results.Created($"/result/{result.Id}", result);
});

app.Run();
