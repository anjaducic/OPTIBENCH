using Microsoft.EntityFrameworkCore;
using Model;

namespace Infrastructure;

public class ResultsContext : DbContext
{
    public DbSet<OptimizationResult> Results { get; set; }
    

    public ResultsContext(DbContextOptions<ResultsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
     
        modelBuilder.Entity<OptimizationResult>()
            .Property(b => b.Params)
            .HasColumnType("json");

        modelBuilder.Entity<OptimizationResult>()
            .Property(b => b.EvaluationCount)
            .HasColumnType("json");

        modelBuilder.Entity<OptimizationResult>()
            .Property(b => b.ProblemInfo)
            .HasColumnType("json");

        //unique dodati po potrebii
    }

   

}
