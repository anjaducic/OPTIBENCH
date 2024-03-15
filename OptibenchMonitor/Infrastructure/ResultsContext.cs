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
        //unique dodati po potrebii
    }

   

}
