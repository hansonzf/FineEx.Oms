using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oms.EntityframeworkCore.Migrations
{
    /// <inheritdoc />
    public partial class oms4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Job_TriggerName",
                table: "Processing",
                newName: "TriggerName");

            migrationBuilder.RenameColumn(
                name: "Job_TriggerGroup",
                table: "Processing",
                newName: "TriggerGroup");

            migrationBuilder.RenameColumn(
                name: "Job_JobName",
                table: "Processing",
                newName: "JobName");

            migrationBuilder.RenameColumn(
                name: "Job_GroupName",
                table: "Processing",
                newName: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_Processing_OrderId",
                table: "Processing",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Processing_Processed_IsScheduled",
                table: "Processing",
                columns: new[] { "Processed", "IsScheduled" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Processing_OrderId",
                table: "Processing");

            migrationBuilder.DropIndex(
                name: "IX_Processing_Processed_IsScheduled",
                table: "Processing");

            migrationBuilder.RenameColumn(
                name: "TriggerName",
                table: "Processing",
                newName: "Job_TriggerName");

            migrationBuilder.RenameColumn(
                name: "TriggerGroup",
                table: "Processing",
                newName: "Job_TriggerGroup");

            migrationBuilder.RenameColumn(
                name: "JobName",
                table: "Processing",
                newName: "Job_JobName");

            migrationBuilder.RenameColumn(
                name: "GroupName",
                table: "Processing",
                newName: "Job_GroupName");
        }
    }
}
