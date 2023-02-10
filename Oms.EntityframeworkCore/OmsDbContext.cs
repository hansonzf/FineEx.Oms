using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Oms.Domain.Handlings;
using Oms.Domain.Orders;
using Oms.Domain.Processings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace Oms.EntityframeworkCore
{
    
    public class OmsDbContext : AbpDbContext<OmsDbContext>
    {
        public OmsDbContext(DbContextOptions<OmsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessOrder>(conf => {
                conf.UseTpcMappingStrategy();
                conf.Ignore(x => x.RelatedOrderIds);
            });

            modelBuilder.Entity<Processing>(conf => {
                conf.HasKey(x => x.Id);
                conf.Ignore(x => x.IsCompleteAllSteps);
                conf.OwnsOne(x => x.Job, d => {
                    d.Property(c => c.JobName).IsRequired().HasColumnName("JobName");
                    d.Property(c => c.GroupName).IsRequired().HasColumnName("GroupName");
                    d.Property(c => c.TriggerName).IsRequired().HasColumnName("TriggerName");
                    d.Property(c => c.TriggerGroup).IsRequired().HasColumnName("TriggerGroup");
                });

                conf.HasIndex(x => new { x.Processed, x.IsScheduled });
                conf.HasIndex(x => x.OrderId);
            });

            #region Configure transport order aggregate
            modelBuilder.Entity<TransportOrder>(conf => {
                conf.ToTable("TransportOrders");
                conf.Property("_orderDetails").HasColumnName("OrderDetails");
                conf.Property(x => x.Visible).HasDefaultValue(true);
                conf.Property(x => x.RelationType).HasDefaultValue(RelationTypes.StandAlone);
                conf.OwnsOne(x => x.Customer, d => {
                    d.Ignore(c => c.CustomerId);
                    d.Property(c => c.CustomerId).HasColumnName("CustomerId");
                    d.Property(c => c.CustomerName).HasColumnName("CustomerName").IsRequired(false);
                });
                conf.OwnsOne(x => x.SenderInfo, d => {
                    d.Property(c => c.AddressId).HasColumnName("SenderAddressId").IsRequired();
                    d.Property(c => c.Contact).HasColumnName("SenderContact").IsRequired();
                    d.Property(c => c.Phone).HasColumnName("SenderPhone").IsRequired();
                    d.Property(c => c.AddressName).HasColumnName("SenderAddressName").IsRequired();
                    d.Property(c => c.Province).HasColumnName("SenderProvince").IsRequired();
                    d.Property(c => c.City).HasColumnName("SenderCity").IsRequired();
                    d.Property(c => c.District).HasColumnName("SenderDistrict").IsRequired();
                    d.Property(c => c.Address).HasColumnName("SenderDetailAddress").IsRequired(false);
                });
                conf.OwnsOne(x => x.ReceiverInfo, d => {
                    d.Property(c => c.AddressId).HasColumnName("ReceiverAddressId").IsRequired();
                    d.Property(c => c.Contact).HasColumnName("ReceiverContact").IsRequired();
                    d.Property(c => c.Phone).HasColumnName("ReceiverPhone").IsRequired();
                    d.Property(c => c.AddressName).HasColumnName("ReceiverAddressName").IsRequired();
                    d.Property(c => c.Province).HasColumnName("ReceiverProvince").IsRequired();
                    d.Property(c => c.City).HasColumnName("ReceiverCity").IsRequired();
                    d.Property(c => c.District).HasColumnName("ReceiverDistrict").IsRequired();
                    d.Property(c => c.Address).HasColumnName("ReceiverDetailAddress").IsRequired(false);
                });
                conf.OwnsOne(x => x.MatchedTransportStrategy, d =>
                {
                    d.Ignore(c => c.TransportResources);
                    d.Ignore(c => c.TransportDetails);
                    d.Property(c => c.Memo).HasColumnName("StrategyMemo").IsRequired(false);
                    d.Property(c => c.TransportLine).HasColumnName("TransportStrategy").IsRequired(false);
                    d.Property(c => c.MatchedTransportLineName).HasColumnName("TransportLineName").IsRequired(false);
                    d.Property(c => c.MatchType).HasColumnName("TransportMatchType");
                    d.Property(c => c.TransportReceipts).HasColumnName("TransportDocuments").IsRequired(false);
                });
                conf.Ignore(x => x.Details);
            });
            #endregion

            #region Configure inbound order aggregate
            modelBuilder.Entity<InboundOrder>(conf => {
                conf.ToTable("InboundOrders");
                conf.Property("_orderDetails").HasColumnName("OrderDetails");
                conf.Property(x => x.Visible).HasDefaultValue(true);
                conf.Property(x => x.RelationType).HasDefaultValue(RelationTypes.StandAlone);
                conf.OwnsOne(x => x.Customer, c => {
                    c.Property(d => d.CustomerId).HasColumnName("CustomerId");
                    c.Property(d => d.CustomerName).HasColumnName("CustomerName").IsRequired(false);
                });
                conf.OwnsOne(x => x.CargoOwner, c => {
                    c.Property(d => d.CargoOwnerId).HasColumnName("CargoOwnerId").HasDefaultValue(0);
                    c.Property(d => d.CargoOwnerName).HasColumnName("CargoOwnerName").IsRequired(false);
                });
                conf.OwnsOne(x => x.Warehouse, c => {
                    c.Property(d => d.WarehouseId).HasColumnName("WarehouseId").IsRequired();
                    c.Property(d => d.InterfaceWarehouseId).HasColumnName("InterfaceWarehouseId").IsRequired();
                    c.Property(d => d.WarehouseName).HasColumnName("WarehouseName");
                });
                conf.OwnsOne(x => x.DeliveryInfo, c => {
                    c.Property(d => d.AddressId).HasColumnName("DeliveryAddressId").IsRequired();
                    c.Property(d => d.Contact).HasColumnName("DeliveryContact").IsRequired();
                    c.Property(d => d.Phone).HasColumnName("DeliveryPhone").IsRequired();
                    c.Property(d => d.AddressName).HasColumnName("DeliveryAddressName").IsRequired();
                    c.Property(d => d.Province).HasColumnName("DeliveryProvince").IsRequired();
                    c.Property(d => d.City).HasColumnName("DeliveryCity").IsRequired();
                    c.Property(d => d.District).HasColumnName("DeliveryDistrict").IsRequired();
                    c.Property(d => d.Address).HasColumnName("DeliveryDetailAddress").IsRequired(false);
                });
                conf.OwnsOne(x => x.MatchedTransportStrategy, c =>
                {
                    c.Ignore(d => d.TransportResources);
                    c.Ignore(d => d.TransportDetails);
                    c.Property(d => d.Memo).HasColumnName("StrategyMemo").IsRequired(false);
                    c.Property(d => d.TransportLine).HasColumnName("TransportStrategy").IsRequired(false);
                    c.Property(d => d.MatchedTransportLineName).HasColumnName("TransportLineName").IsRequired(false);
                    c.Property(d => d.MatchType).HasColumnName("TransportMatchType");
                    c.Property(d => d.TransportReceipts).HasColumnName("TransportDocuments").IsRequired(false);
                });
                conf.Ignore(x => x.Details);
            });
            #endregion

            #region Configure outbound order aggregate
            modelBuilder.Entity<OutboundOrder>(conf => { 
                conf.ToTable("OutboundOrders");
                conf.Property("_orderDetails").HasColumnName("OrderDetails");
                conf.Property("_relatedOrderIds").HasColumnName("RelateOrderIds");
                conf.Property(x => x.Visible).HasDefaultValue(true);
                conf.Property(x => x.RelationType).HasDefaultValue(RelationTypes.StandAlone);
                conf.Property(x => x.ExternalOrderNumber).HasDefaultValue("");
                conf.OwnsOne(x => x.Customer, c => {
                    c.Property(d => d.CustomerId).HasColumnName("CustomerId");
                    c.Property(d => d.CustomerName).HasColumnName("CustomerName").IsRequired(false);
                });
                conf.OwnsOne(x => x.CargoOwner, c => {
                    c.Property(d => d.CargoOwnerId).HasColumnName("CargoOwnerId").HasDefaultValue(0);
                    c.Property(d => d.CargoOwnerName).HasColumnName("CargoOwnerName").IsRequired(false);
                });
                conf.OwnsOne(x => x.Warehouse, c => {
                    c.Property(d => d.WarehouseId).HasColumnName("WarehouseId").IsRequired();
                    c.Property(d => d.InterfaceWarehouseId).HasColumnName("InterfaceWarehouseId").HasDefaultValue(0).IsRequired();
                    c.Property(d => d.WarehouseName).HasColumnName("WarehouseName");
                });
                conf.OwnsOne(x => x.DeliveryInfo, c => {
                    c.Property(d => d.AddressId).HasColumnName("DeliveryAddressId").IsRequired();
                    c.Property(d => d.Contact).HasColumnName("DeliveryContact").IsRequired();
                    c.Property(d => d.Phone).HasColumnName("DeliveryPhone").IsRequired();
                    c.Property(d => d.AddressName).HasColumnName("DeliveryAddressName").IsRequired();
                    c.Property(d => d.Province).HasColumnName("DeliveryProvince").IsRequired();
                    c.Property(d => d.City).HasColumnName("DeliveryCity").IsRequired();
                    c.Property(d => d.District).HasColumnName("DeliveryDistrict").IsRequired();
                    c.Property(d => d.Address).HasColumnName("DeliveryDetailAddress").IsRequired(false);
                });
                conf.OwnsOne(x => x.MatchedTransportStrategy, c =>
                {
                    c.Ignore(d => d.TransportResources);
                    c.Ignore(d => d.TransportDetails);
                    c.Property(d => d.Memo).HasColumnName("StrategyMemo").IsRequired(false);
                    c.Property(d => d.TransportLine).HasColumnName("TransportStrategy").IsRequired(false);
                    c.Property(d => d.MatchedTransportLineName).HasColumnName("TransportLineName").IsRequired(false);
                    c.Property(d => d.MatchType).HasColumnName("TransportMatchType");
                    c.Property(d => d.TransportReceipts).HasColumnName("TransportDocuments").IsRequired(false);
                });
                conf.Ignore(x => x.Details);
            });

            #endregion


            modelBuilder.Entity<Handling>(conf => {
                conf.HasKey(x => x.Id);
                conf.HasIndex(x => x.OrderId);
            });
        }
    }
}
