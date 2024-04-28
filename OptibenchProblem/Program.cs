using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "OptibenchProblem API", Description = "Testing OptibenchProblem", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "OptibenchProblem API V1");
});


//http://localhost:5030/problems/spherical?x=1&x=1.2&x=-0.5&x=0 - primjer izgleda putanje
app.MapGet("/problems/{problem_name}", (string problem_name, double[] x) => {

   switch(problem_name)
   {
      case "Spherical":
      {
         double fx = MathFunctions.Sphere(x);
         if(double.IsNaN(fx))
            return Results.NotFound("No result.");
         return Results.Ok(fx);
      }         
      case "Rosenbrock":
      {
         double fx = MathFunctions.Rosenbrock(x);
         if(double.IsNaN(fx))
            return Results.NotFound("No result.");
         return Results.Ok(fx);
      }
      case "Rastrigin":
      {
         double fx = MathFunctions.Rastrigin(x);
         if(double.IsNaN(fx))
            return Results.NotFound("No result.");
         return Results.Ok(fx);
      }
      case "Shekel":
      {
         double fx = MathFunctions.Shekel(x);
         if(double.IsNaN(fx))
            return Results.NotFound("No result.");
         return Results.Ok(fx);
      }
      case "Matyas":
      {
         double fx = MathFunctions.Matyas(x);
         if(double.IsNaN(fx))
            return Results.NotFound("No result.");
         return Results.Ok(fx);
      }
      case "Easom":
      {
         double fx = MathFunctions.Easom(x);
         if(double.IsNaN(fx))
            return Results.NotFound("No result.");
         return Results.Ok(fx);
      }
        
      //sa ogranicenjima:
      case "GomezLevi":
      {
         double fx = MathFunctions.GomezLevi(x);
         if(double.IsNaN(fx))
            return Results.NotFound("No result.");
         return Results.Ok(fx);
      }
      case "MishrasBird":
      {
         double fx = MathFunctions.MishrasBird(x);
         if(double.IsNaN(fx))
            return Results.NotFound("No result.");
         return Results.Ok(fx);
      }
         
      default:
         return Results.NotFound("Problem not found.");
   }

});


app.MapGet("/exact-solution/{problem_name}", (string problem_name) => {

   switch(problem_name)
   {
      case "Spherical":
      {
         return Results.Ok(0.0);
      }         
      case "Rosenbrock":
      {
         return Results.Ok(0.0);
      }
      case "Rastrigin":
      {
         return Results.Ok(0.0);
      }
      case "Shekel":
      {
         return Results.Ok(double.NaN);   //ok jer postoji fja, ali nepoznato rj
      }
      case "Matyas":
      {
         return Results.Ok(0.0);
      }
      case "Easom":
      {
         return Results.Ok(-1.0);
      }
        
      //sa ogranicenjima:
      case "GomezLevi":
      {
         return Results.Ok(-1.031628453);
      }
      case "MishrasBird":
      {
         return Results.Ok(-106.7645367);
      }
         
      default:
         return Results.NotFound("Problem not found.");
   }

});







app.Run();
