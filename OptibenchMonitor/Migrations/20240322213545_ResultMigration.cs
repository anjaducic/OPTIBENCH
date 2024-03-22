using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OptibenchMonitor.Migrations
{
    /// <inheritdoc />
    public partial class ResultMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    X = table.Column<double[]>(type: "double precision[]", nullable: false),
                    Y = table.Column<double>(type: "double precision", nullable: false),
                    Params = table.Column<string>(type: "json", nullable: false),
                    ProblemInfo = table.Column<string>(type: "json", nullable: false),
                    EvaluationCount = table.Column<string>(type: "json", nullable: false),
                    OptimizerName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");
        }
    }
}
