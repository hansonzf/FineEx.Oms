using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oms.EntityframeworkCore.Migrations
{
    /// <inheritdoc />
    public partial class oms2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StrategyMemo",
                table: "TransportOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrategyMemo",
                table: "OutboundOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrategyMemo",
                table: "InboundOrders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StrategyMemo",
                table: "TransportOrders");

            migrationBuilder.DropColumn(
                name: "StrategyMemo",
                table: "OutboundOrders");

            migrationBuilder.DropColumn(
                name: "StrategyMemo",
                table: "InboundOrders");
        }
    }
}
