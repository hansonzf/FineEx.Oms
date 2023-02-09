using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oms.EntityframeworkCore.Migrations
{
    /// <inheritdoc />
    public partial class oms1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Handling",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExecuteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HandleBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handling", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InboundOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderSource = table.Column<int>(type: "int", nullable: false),
                    BusinessType = table.Column<int>(type: "int", nullable: false),
                    ReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RelationType = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    OrderState = table.Column<int>(type: "int", nullable: false),
                    ExpectingCompleteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FactCompleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InboundId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargoOwnerId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CargoOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryAddressId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryDetailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReturnOrder = table.Column<bool>(type: "bit", nullable: false),
                    OriginDeliveryNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    InterfaceWarehouseId = table.Column<int>(type: "int", nullable: false),
                    WarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InboundType = table.Column<int>(type: "int", nullable: false),
                    OrderDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportLineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportMatchType = table.Column<int>(type: "int", nullable: true),
                    TransportStrategy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportDocuments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboundOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboundOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderSource = table.Column<int>(type: "int", nullable: false),
                    BusinessType = table.Column<int>(type: "int", nullable: false),
                    ReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RelationType = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    OrderState = table.Column<int>(type: "int", nullable: false),
                    ExpectingCompleteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FactCompleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OutboundId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargoOwnerId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CargoOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryAddressId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryDetailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryType = table.Column<int>(type: "int", nullable: false),
                    OutboundType = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    InterfaceWarehouseId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    WarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CombinationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpectingOutboundTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FactOutboundTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelateOrderIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportLineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportMatchType = table.Column<int>(type: "int", nullable: true),
                    TransportStrategy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportDocuments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboundOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Processing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessType = table.Column<int>(type: "int", nullable: false),
                    Steps = table.Column<int>(type: "int", nullable: false),
                    Processed = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastTaskBuiltAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JobJobName = table.Column<string>(name: "Job_JobName", type: "nvarchar(max)", nullable: true),
                    JobGroupName = table.Column<string>(name: "Job_GroupName", type: "nvarchar(max)", nullable: true),
                    JobTriggerName = table.Column<string>(name: "Job_TriggerName", type: "nvarchar(max)", nullable: true),
                    IsScheduled = table.Column<bool>(type: "bit", nullable: false),
                    ExecutedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderSource = table.Column<int>(type: "int", nullable: false),
                    BusinessType = table.Column<int>(type: "int", nullable: false),
                    ReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RelationType = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    OrderState = table.Column<int>(type: "int", nullable: false),
                    ExpectingCompleteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FactCompleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransportId = table.Column<long>(type: "bigint", nullable: false),
                    IsReturnBack = table.Column<bool>(type: "bit", nullable: false),
                    TransportType = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderAddressId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderAddressName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderDetailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverAddressId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverAddressName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverDetailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsignState = table.Column<int>(type: "int", nullable: false),
                    ExpectingPickupTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FactPickupTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportLineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportMatchType = table.Column<int>(type: "int", nullable: true),
                    TransportStrategy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportDocuments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportOrders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Handling_OrderId",
                table: "Handling",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Handling");

            migrationBuilder.DropTable(
                name: "InboundOrders");

            migrationBuilder.DropTable(
                name: "OutboundOrders");

            migrationBuilder.DropTable(
                name: "Processing");

            migrationBuilder.DropTable(
                name: "TransportOrders");
        }
    }
}
