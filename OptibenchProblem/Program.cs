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
         return MathFunctions.Sphere(x);
      case "Rosenbrock":
         return MathFunctions.Rosenbrock(x);
      case "Rastrigin":
         return MathFunctions.Rastrigin(x);
      case "Shekel":
         return MathFunctions.Shekel(x);
      case "Matyas":
         return MathFunctions.Matyas(x);
      case "Easom":
         return MathFunctions.Easom(x);
      //sa ogranicenjima:
      case "GomezLevi":
         return MathFunctions.GomezLevi(x);
      case "MishrasBird":
         return MathFunctions.MishrasBird(x);
      default:
         return double.MaxValue;
   }

});







app.Run();
