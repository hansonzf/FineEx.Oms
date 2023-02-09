using Microsoft.AspNetCore.Mvc;
using Oms.Application.Contracts.CollaborationServices.ThreePL;
using Oms.Domain.Orders;
using Oms.HttpApi.Models;
using System.Net;

namespace Oms.HttpApi.Controllers
{
    [ApiController]
    [Route("api/dictionary")]
    public class DictionaryController : BaseController
    {
        readonly IThreePLService service;

        public DictionaryController(IThreePLService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 查询承运商列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCarrierList")]
        public async Task<IActionResult> GetCarrierList()
        {
            var allCarriers = await service.GetCarrier(CurrentUser.TenantId);
            if (allCarriers.Any())
                return Ok(RspModel.Success(allCarriers.Select(s => new { Key = s.ResourceId, Name = s.Name })));
            else
                return NotFound();
        }

        /// <summary>
        /// 查询网点列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetNetworkList")]
        public async Task<IActionResult> GetNetworkList()
        {
            var allNetwork = await service.GetLogisticsCenter(CurrentUser.TenantId);
            var networkList = allNetwork
                              .Select(s => new
                              {
                                  AddressName = s.Name,
                                  AddressId = s.ResourceId,
                                  Contacter = s.Contact,
                                  Phone = s.Phone,
                                  Province = s.Province,
                                  City = s.City,
                                  Area = s.District,
                                  Address = s.Address
                              })
                              .ToList();

            return networkList.Any() ? Ok(RspModel.Success(networkList)) : NotFound();
        }

        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCustomerList")]
        public async Task<IActionResult> GetCustomerList()
        {
            List<CustomerDescription> customer = await service.GetCustomer(CurrentUser.TenantId);
            var resp = RspModel.Success(customer);
            return customer.Any() ? Ok(resp) : NotFound();
        }

        /// <summary>
        /// 获取地址列表
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("GetAddressList")]
        public async Task<RspModel<List<RspAddress>>> GetAddressList(string customerId)
        {
            var allAddress = await service.GetAddressByCustomer(CurrentUser.TenantId, customerId);
            var resp = allAddress.Select(a => new RspAddress { 
                Address= a.Address,
                AddressId= a.AddressId,
                City = a.City,
                AddressName= a.AddressName,
                Area = a.District,
                Contacter = a.Contact,
                Phone = a.Phone,
                Province = a.Province
            }).ToList();
            return RspModel.Success<List<RspAddress>>(resp);
        }

        /// <summary>
        /// 获取仓库列表
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("GetWarehouseList")]
        public async Task<IActionResult> GetWarehouseList(int consignerId)
        {
            List<WarehouseDescription> systemWarehouseDtos = await service.GetWarehouse(CurrentUser.TenantId, consignerId);
            var warehouseLists = systemWarehouseDtos.Select(s => new {
                s.WarehouseId,
                s.InterfaceWarehouseId, 
                s.WarehouseName
            }).ToList();

            return warehouseLists.Any() ? Ok(RspModel.Success(warehouseLists)) : NoContent();
        }

        /// <summary>
        /// 获取省市区树形列表
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("GetRegionList")]
        public async Task<IActionResult> GetRegionList()
        {
            var regions = await service.GetRegion(CurrentUser.TenantId);

            return regions.Any() ? Ok(RspModel.Success(regions)) : NoContent();
        }
    }
}
