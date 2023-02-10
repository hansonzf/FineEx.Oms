using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oms.EntityframeworkCore.Migrations
{
    /// <inheritdoc />
    public partial class oms3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Job_TriggerGroup",
                table: "Processing",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Job_TriggerGroup",
                table: "Processing");
        }
    }
}
