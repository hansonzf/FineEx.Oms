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
                conf.OwnsOne(x => x.Job);
            });

            #region Configure transport order aggregate
            modelBuilder.Entity<TransportOrder>(conf => {
                conf.ToTable("TransportOrders");
                conf.Property("_orderDetails").HasColumnName("OrderDetails");
                conf.Property(x => x.Visible).HasDefaultValue(true);
                conf.Property(x => x.RelationType).HasDefaultValue(RelationTypes.StandAlone);
                conf.OwnsOne(x => x.Customer, c => {
                    c.Ignore(d => d.CustomerId);
                    c.Property(d => d.CustomerId).HasColumnName("CustomerId");
                    c.Property(d => d.CustomerName).HasColumnName("CustomerName").IsRequired(false);
                });
                conf.OwnsOne(x => x.SenderInfo, c => {
                    c.Property(d => d.AddressId).HasColumnName("SenderAddressId").IsRequired();
                    c.Property(d => d.Contact).HasColumnName("SenderContact").IsRequired();
                    c.Property(d => d.Phone).HasColumnName("SenderPhone").IsRequired();
                    c.Property(d => d.AddressName).HasColumnName("SenderAddressName").IsRequired();
                    c.Property(d => d.Province).HasColumnName("SenderProvince").IsRequired();
                    c.Property(d => d.City).HasColumnName("SenderCity").IsRequired();
                    c.Property(d => d.District).HasColumnName("SenderDistrict").IsRequired();
                    c.Property(d => d.Address).HasColumnName("SenderDetailAddress").IsRequired(false);
                });
                conf.OwnsOne(x => x.ReceiverInfo, c => {
                    c.Property(d => d.AddressId).HasColumnName("ReceiverAddressId").IsRequired();
                    c.Property(d => d.Contact).HasColumnName("ReceiverContact").IsRequired();
                    c.Property(d => d.Phone).HasColumnName("ReceiverPhone").IsRequired();
                    c.Property(d => d.AddressName).HasColumnName("ReceiverAddressName").IsRequired();
                    c.Property(d => d.Province).HasColumnName("ReceiverProvince").IsRequired();
                    c.Property(d => d.City).HasColumnName("ReceiverCity").IsRequired();
                    c.Property(d => d.District).HasColumnName("ReceiverDistrict").IsRequired();
                    c.Property(d => d.Address).HasColumnName("ReceiverDetailAddress").IsRequired(false);
                });
                conf.OwnsOne(x => x.MatchedTransportStrategy, c =>
                {
                    c.Ignore(d => d.TransportResources);
                    c.Ignore(d => d.TransportDetails);
                    c.Property(d => d.TransportLine).HasColumnName("TransportStrategy").IsRequired(false);
                    c.Property(d => d.MatchedTransportLineName).HasColumnName("TransportLineName").IsRequired(false);
                    c.Property(d => d.MatchType).HasColumnName("TransportMatchType");
                    c.Property(d => d.TransportReceipts).HasColumnName("TransportDocuments").IsRequired(false);
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
