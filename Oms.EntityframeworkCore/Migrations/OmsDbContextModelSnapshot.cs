﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oms.EntityframeworkCore;

#nullable disable

namespace Oms.EntityframeworkCore.Migrations
{
    [DbContext(typeof(OmsDbContext))]
    partial class OmsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Oms.Domain.Handlings.Handling", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExecuteTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("HandleBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Handling");
                });

            modelBuilder.Entity("Oms.Domain.Orders.BusinessOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BusinessType")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpectingCompleteTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExternalOrderNumber")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<DateTime?>("FactCompleteTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderSource")
                        .HasColumnType("int");

                    b.Property<int>("OrderState")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReceivedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("RelationType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("TenantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Visible")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Oms.Domain.Processings.Processing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BusinessType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExecutedCount")
                        .HasColumnType("int");

                    b.Property<bool>("IsScheduled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastTaskBuiltAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Processed")
                        .HasColumnType("int");

                    b.Property<long>("SerialNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("Steps")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Processing");
                });

            modelBuilder.Entity("Oms.Domain.Orders.InboundOrder", b =>
                {
                    b.HasBaseType("Oms.Domain.Orders.BusinessOrder");

                    b.Property<long>("InboundId")
                        .HasColumnType("bigint");

                    b.Property<int>("InboundType")
                        .HasColumnType("int");

                    b.Property<bool>("IsReturnOrder")
                        .HasColumnType("bit");

                    b.Property<string>("OriginDeliveryNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_orderDetails")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OrderDetails");

                    b.ToTable("InboundOrders", (string)null);
                });

            modelBuilder.Entity("Oms.Domain.Orders.OutboundOrder", b =>
                {
                    b.HasBaseType("Oms.Domain.Orders.BusinessOrder");

                    b.Property<string>("CombinationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DeliveryType")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpectingOutboundTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FactOutboundTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("OutboundId")
                        .HasColumnType("bigint");

                    b.Property<int>("OutboundType")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_orderDetails")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OrderDetails");

                    b.Property<string>("_relatedOrderIds")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("RelateOrderIds");

                    b.ToTable("OutboundOrders", (string)null);
                });

            modelBuilder.Entity("Oms.Domain.Orders.TransportOrder", b =>
                {
                    b.HasBaseType("Oms.Domain.Orders.BusinessOrder");

                    b.Property<int>("ConsignState")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpectingPickupTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FactPickupTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsReturnBack")
                        .HasColumnType("bit");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TransportId")
                        .HasColumnType("bigint");

                    b.Property<int>("TransportType")
                        .HasColumnType("int");

                    b.Property<string>("_orderDetails")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OrderDetails");

                    b.ToTable("TransportOrders", (string)null);
                });

            modelBuilder.Entity("Oms.Domain.Processings.Processing", b =>
                {
                    b.OwnsOne("Oms.Domain.Processings.ProcessingJob", "Job", b1 =>
                        {
                            b1.Property<Guid>("ProcessingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("GroupName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("JobName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("TriggerName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProcessingId");

                            b1.ToTable("Processing");

                            b1.WithOwner()
                                .HasForeignKey("ProcessingId");
                        });

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Oms.Domain.Orders.InboundOrder", b =>
                {
                    b.OwnsOne("Oms.Domain.Orders.CargoOwnerDescription", "CargoOwner", b1 =>
                        {
                            b1.Property<Guid>("InboundOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("CargoOwnerId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasDefaultValue(0)
                                .HasColumnName("CargoOwnerId");

                            b1.Property<string>("CargoOwnerName")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CargoOwnerName");

                            b1.HasKey("InboundOrderId");

                            b1.ToTable("InboundOrders");

                            b1.WithOwner()
                                .HasForeignKey("InboundOrderId");
                        });

                    b.OwnsOne("Oms.Domain.Orders.CustomerDescription", "Customer", b1 =>
                        {
                            b1.Property<Guid>("InboundOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("CustomerId")
                                .HasColumnType("int")
                                .HasColumnName("CustomerId");

                            b1.Property<string>("CustomerName")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CustomerName");

                            b1.HasKey("InboundOrderId");

                            b1.ToTable("InboundOrders");

                            b1.WithOwner()
                                .HasForeignKey("InboundOrderId");
                        });

                    b.OwnsOne("Oms.Domain.Orders.AddressDescription", "DeliveryInfo", b1 =>
                        {
                            b1.Property<Guid>("InboundOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryDetailAddress");

                            b1.Property<string>("AddressId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryAddressId");

                            b1.Property<string>("AddressName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryAddressName");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryCity");

                            b1.Property<string>("Contact")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryContact");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryDistrict");

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryPhone");

                            b1.Property<string>("Province")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryProvince");

                            b1.HasKey("InboundOrderId");

                            b1.ToTable("InboundOrders");

                            b1.WithOwner()
                                .HasForeignKey("InboundOrderId");
                        });

                    b.OwnsOne("Oms.Domain.Orders.TransportStrategy", "MatchedTransportStrategy", b1 =>
                        {
                            b1.Property<Guid>("InboundOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("MatchType")
                                .HasColumnType("int")
                                .HasColumnName("TransportMatchType");

                            b1.Property<string>("MatchedTransportLineName")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TransportLineName");

                            b1.Property<string>("Memo")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("StrategyMemo");

                            b1.Property<string>("TransportLine")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TransportStrategy");

                            b1.Property<string>("TransportReceipts")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TransportDocuments");

                            b1.HasKey("InboundOrderId");

                            b1.ToTable("InboundOrders");

                            b1.WithOwner()
                                .HasForeignKey("InboundOrderId");
                        });

                    b.OwnsOne("Oms.Domain.Orders.WarehouseDescription", "Warehouse", b1 =>
                        {
                            b1.Property<Guid>("InboundOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("InterfaceWarehouseId")
                                .HasColumnType("int")
                                .HasColumnName("InterfaceWarehouseId");

                            b1.Property<int>("WarehouseId")
                                .HasColumnType("int")
                                .HasColumnName("WarehouseId");

                            b1.Property<string>("WarehouseName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("WarehouseName");

                            b1.HasKey("InboundOrderId");

                            b1.ToTable("InboundOrders");

                            b1.WithOwner()
                                .HasForeignKey("InboundOrderId");
                        });

                    b.Navigation("CargoOwner")
                        .IsRequired();

                    b.Navigation("Customer")
                        .IsRequired();

                    b.Navigation("DeliveryInfo")
                        .IsRequired();

                    b.Navigation("MatchedTransportStrategy");

                    b.Navigation("Warehouse")
                        .IsRequired();
                });

            modelBuilder.Entity("Oms.Domain.Orders.OutboundOrder", b =>
                {
                    b.OwnsOne("Oms.Domain.Orders.CargoOwnerDescription", "CargoOwner", b1 =>
                        {
                            b1.Property<Guid>("OutboundOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("CargoOwnerId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasDefaultValue(0)
                                .HasColumnName("CargoOwnerId");

                            b1.Property<string>("CargoOwnerName")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CargoOwnerName");

                            b1.HasKey("OutboundOrderId");

                            b1.ToTable("OutboundOrders");

                            b1.WithOwner()
                                .HasForeignKey("OutboundOrderId");
                        });

                    b.OwnsOne("Oms.Domain.Orders.CustomerDescription", "Customer", b1 =>
                        {
                            b1.Property<Guid>("OutboundOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("CustomerId")
                                .HasColumnType("int")
                                .HasColumnName("CustomerId");

                            b1.Property<string>("CustomerName")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CustomerName");

                            b1.HasKey("OutboundOrderId");

                            b1.ToTable("OutboundOrders");

                            b1.WithOwner()
                                .HasForeignKey("OutboundOrderId");
                        });

                    b.OwnsOne("Oms.Domain.Orders.AddressDescription", "DeliveryInfo", b1 =>
                        {
                            b1.Property<Guid>("OutboundOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryDetailAddress");

                            b1.Property<string>("AddressId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryAddressId");

                            b1.Property<string>("AddressName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryAddressName");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryCity");

                            b1.Property<string>("Contact")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryContact");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryDistrict");

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryPhone");

                            b1.Property<string>("Province")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DeliveryProvince");

                            b1.HasKey("OutboundOrderId");

                            b1.ToTable("OutboundOrders");

                            b1.WithOwner()
                                .HasForeignKey("OutboundOrderId");
                        });

                    b.OwnsOne("Oms.Domain.Orders.TransportStrategy", "MatchedTransportStrategy", b1 =>
                        {
                            b1.Property<Guid>("OutboundOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("MatchType")
                                .HasColumnType("int")
                                .HasColumnName("TransportMatchType");

                            b1.Property<string>("MatchedTransportLineName")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TransportLineName");

                            b1.Property<string>("Memo")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("StrategyMemo");

                            b1.Property<string>("TransportLine")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TransportStrategy");

                            b1.Property<string>("TransportReceipts")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TransportDocuments");

                            b1.HasKey("OutboundOrderId");

                            b1.ToTable("OutboundOrders");

                            b1.WithOwner()
                                .HasForeignKey("OutboundOrderId");
                        });

                    b.OwnsOne("Oms.Domain.Orders.WarehouseDescription", "Warehouse", b1 =>
                        {
                            b1.Property<Guid>("OutboundOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("InterfaceWarehouseId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasDefaultValue(0)
                                .HasColumnName("InterfaceWarehouseId");

                            b1.Property<int>("WarehouseId")
                                .HasColumnType("int")
                                .HasColumnName("WarehouseId");

                            b1.Property<string>("WarehouseName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("WarehouseName");

                            b1.HasKey("OutboundOrderId");

                            b1.ToTable("OutboundOrders");

                            b1.WithOwner()
                                .HasForeignKey("OutboundOrderId");
                        });

                    b.Navigation("CargoOwner")
                        .IsRequired();

                    b.Navigation("Customer")
                        .IsRequired();

                    b.Navigation("DeliveryInfo")
                        .IsRequired();

                    b.Navigation("MatchedTransportStrategy");

                    b.Navigation("Warehouse")
                        .IsRequired();
                });

            modelBuilder.Entity("Oms.Domain.Orders.TransportOrder", b =>
                {
                    b.OwnsOne("Oms.Domain.Orders.CustomerDescription", "Customer", b1 =>
                        {
                            b1.Property<Guid>("TransportOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("CustomerId")
                                .HasColumnType("int")
                                .HasColumnName("CustomerId");

                            b1.Property<string>("CustomerName")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CustomerName");

                            b1.HasKey("TransportOrderId");

                            b1.ToTable("TransportOrders");

                            b1.WithOwner()
                                .HasForeignKey("TransportOrderId");
                        });

                    b.OwnsOne("Oms.Domain.Orders.TransportStrategy", "MatchedTransportStrategy", b1 =>
                        {
                            b1.Property<Guid>("TransportOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("MatchType")
                                .HasColumnType("int")
                                .HasColumnName("TransportMatchType");

                            b1.Property<string>("MatchedTransportLineName")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TransportLineName");

                            b1.Property<string>("Memo")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("StrategyMemo");

                            b1.Property<string>("TransportLine")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TransportStrategy");

                            b1.Property<string>("TransportReceipts")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TransportDocuments");

                            b1.HasKey("TransportOrderId");

                            b1.ToTable("TransportOrders");

                            b1.WithOwner()
                                .HasForeignKey("TransportOrderId");
                        });

                    b.OwnsOne("Oms.Domain.Orders.AddressDescription", "ReceiverInfo", b1 =>
                        {
                            b1.Property<Guid>("TransportOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ReceiverDetailAddress");

                            b1.Property<string>("AddressId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ReceiverAddressId");

                            b1.Property<string>("AddressName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ReceiverAddressName");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ReceiverCity");

                            b1.Property<string>("Contact")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ReceiverContact");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ReceiverDistrict");

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ReceiverPhone");

                            b1.Property<string>("Province")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ReceiverProvince");

                            b1.HasKey("TransportOrderId");

                            b1.ToTable("TransportOrders");

                            b1.WithOwner()
                                .HasForeignKey("TransportOrderId");
                        });

                    b.OwnsOne("Oms.Domain.Orders.AddressDescription", "SenderInfo", b1 =>
                        {
                            b1.Property<Guid>("TransportOrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("SenderDetailAddress");

                            b1.Property<string>("AddressId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("SenderAddressId");

                            b1.Property<string>("AddressName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("SenderAddressName");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("SenderCity");

                            b1.Property<string>("Contact")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("SenderContact");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("SenderDistrict");

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("SenderPhone");

                            b1.Property<string>("Province")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("SenderProvince");

                            b1.HasKey("TransportOrderId");

                            b1.ToTable("TransportOrders");

                            b1.WithOwner()
                                .HasForeignKey("TransportOrderId");
                        });

                    b.Navigation("Customer")
                        .IsRequired();

                    b.Navigation("MatchedTransportStrategy");

                    b.Navigation("ReceiverInfo")
                        .IsRequired();

                    b.Navigation("SenderInfo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
