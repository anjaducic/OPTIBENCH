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
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "OptibenchProcess API V1");
});


//http://localhost:5030/problems/spherical?x=1&x=1.2&x=-0.5&x=0 - primjer izgleda putanje
app.MapGet("/problems/{problem_name}", (string problem_name, double[] x) => {

   switch(problem_name)
   {
      case "spherical":
         return MathFunctions.Sphere(x);
      case "rosenbrock":
         return MathFunctions.Rosenbrock(x);
      case "rastrigin":
         return MathFunctions.Rastrigin(x);
      case "shekel":
         return MathFunctions.Shekel(x);
      default:
         return -1;
   }

});







app.Run();
