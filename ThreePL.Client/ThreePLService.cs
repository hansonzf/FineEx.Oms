using Newtonsoft.Json;
using Oms.Application.Contracts;
using Oms.Application.Contracts.CollaborationServices.ThreePL;
using Oms.Domain.Orders;
using RestSharp;
using System;

namespace ThreePL.Client
{
    public class ThreePLService : IThreePLService
    {
        public readonly HttpClient httpClient;

        public ThreePLService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("3pl");
            //httpClient.BaseAddress = new Uri("http://10.32.18.116:8086/");
            httpClient.BaseAddress = new Uri("https://webtest.3pl.fineyun.cn:9012");
        }

        public async Task<List<CustomerCargoOwner>> GetCustomerCargoOwner(string tenantId)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }

            var customerRes = await httpClient.GetStringAsync("/api/customer-management/customer/all");
            var customers = JsonConvert.DeserializeObject<List<CustomerDto>>(customerRes);
            var result = customers.Select(c => new CustomerCargoOwner { Customer = new CustomerDescription(c.TripleId, c.Name) }).ToList();
            var cargoOwnerRes = await httpClient.GetStringAsync($@"/api/consigner-management/consigner/all");
            var cargoOwners = JsonConvert.DeserializeObject<ListDto<List<ConsignerDto>>>(cargoOwnerRes);

            foreach (var customer in result)
            {
                customer.CargoOwners = cargoOwners.Items
                    .Where(o => o.CustomerTripleId == customer.Customer.CustomerId)
                    .Select(o => new CargoOwnerDescription(o.TripleId ?? 0, o.Name))
                    .ToList();
            }

            return result;
        }

        public async Task<AddressDescription?> GetWarehouseAddress(string tenantId, WarehouseDescription warehouse)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }
            var res = await httpClient.GetStringAsync($@"/api/administration/location/detail?addressType=1&systemWarehouseId={warehouse.WarehouseId}");
            var resp = JsonConvert.DeserializeObject<LocationDto>(res);
            return resp is not null ?
                new AddressDescription(resp.Id, resp.Contact, resp.ContactPhone, resp.Name, resp.Province, resp.City, resp.District, resp.DetailAddress) :
                null;
        }

        public async Task<List<AddressDescription>> GetAddress(string tenantId)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }
            string url = $@"/api/administration/location/all";
            var res = await httpClient.GetStringAsync(url);
            var resp = JsonConvert.DeserializeObject<ListDto<List<LocationDto>>>(res);
            return resp.Items.Any() ?
                resp.Items.Select(r => new AddressDescription(r.Id, r.Contact, r.ContactPhone, r.Name, r.Province, r.City, r.District, r.DetailAddress)).ToList() :
                new List<AddressDescription>();
        }

        public async Task<List<AddressDescription>> GetAddressByCustomer(string tenantId, string customerId)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }
            string url = $@"/api/administration/location/all";
            var res = await httpClient.GetStringAsync(url);
            var resp = JsonConvert.DeserializeObject<ListDto<List<LocationDto>>>(res);
            List<AddressDescription> result = new List<AddressDescription>();
            foreach (var item in resp.Items)
            {
                if (!string.IsNullOrEmpty(item.CustomerId?.ToString()) && item.CustomerId?.ToString() != customerId)
                    continue;

                var address = new AddressDescription(item.Id, item.Contact, item.ContactPhone, item.Name, item.Province, item.City, item.District, item.DetailAddress);
                result.Add(address);
            }

            return result;
        }

        public async Task<List<CargoOwnerDescription>> GetCargoOwner(string tenantId, string customerId)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }
            string url = $@"/api/consigner-management/{customerId}/consigner";
            var res = await httpClient.GetStringAsync(url);
            var resp = JsonConvert.DeserializeObject<ListDto<List<ConsignerDto>>>(res);
            return resp.Items.Any() ?
                resp.Items.Select(r => new CargoOwnerDescription(r.TripleId.HasValue ? r.TripleId.Value : 0, r.Name)).ToList() :
                new List<CargoOwnerDescription>();
        }

        public async Task<List<CargoOwnerDescription>> GetCargoOwner(string tenantId)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }
            string url = $@"/api/consigner-management/consigner/all";
            var res = await httpClient.GetStringAsync(url);
            var resp = JsonConvert.DeserializeObject<ListDto<List<ConsignerDto>>>(res);
            return resp.Items.Any() ?
                resp.Items.Select(r => new CargoOwnerDescription(r.TripleId.HasValue ? r.TripleId.Value : 0, r.Name)).ToList() :
                new List<CargoOwnerDescription>();
        }

        public async Task<List<TransportResource>> GetCarrier(string tenantId)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }
            string url = $@"/api/administration/transport/all";
            var res = await httpClient.GetStringAsync(url);
            var respObj = JsonConvert.DeserializeObject<ListDto<List<LogisticsCenterDto>>>(res);
            if (respObj.Items.Any())
                return respObj.Items.Select(i => new TransportResource
                {
                    ResourceId = i.Id,
                    Type = TransportResourceTypes.Vendor,
                    Name = i.Name,
                    Contact = i.Contact,
                    Phone = i.ContactPhone
                }).ToList();
            else
                return new List<TransportResource>();
        }

        public async Task<List<CustomerDescription>> GetCustomer(string tenantId)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }
            var res = await httpClient.GetStringAsync("/api/customer-management/customer/all");
            var respObj = JsonConvert.DeserializeObject<List<CustomerDto>>(res);
            if (respObj.Any())
                return respObj.Select(c => new CustomerDescription(c.TripleId, c.Name)).ToList();
            else
                return new List<CustomerDescription>();
        }

        public async Task<List<TransportResource>> GetLogisticsCenter(string tenantId)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }
            string url = $@"/api/administration/logisticscenter/all";
            var res = await httpClient.GetStringAsync(url);
            var respObj = JsonConvert.DeserializeObject<ListDto<List<LogisticsCenterDto>>>(res) ?? new ListDto<List<LogisticsCenterDto>>();
            if (respObj.Items.Any())
                return respObj.Items.Select(i => new TransportResource
                {
                    ResourceId = i.Id,
                    Type = TransportResourceTypes.LogisticsCenter,
                    Name = i.Name,
                    Contact = i.Contact,
                    Phone = i.ContactPhone,
                    Province = i.Province,
                    City = i.City,
                    District = i.District,
                    Address = i.Address
                }).ToList();
            else
                return new List<TransportResource>();
        }

        public async Task<TransportResource> GetLogisticsCenter(string tenantId, string logisticKey)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }
            string url = $@"/api/administration/logisticscenter/{logisticKey}";
            var res = await httpClient.GetStringAsync(url);
            if (res == null) return null;
            var respObj = JsonConvert.DeserializeObject<LogisticsCenterDto>(res);

            return new TransportResource
            {
                ResourceId = respObj.Id,
                Type = TransportResourceTypes.LogisticsCenter,
                Name = respObj.Name,
                Contact = respObj.Contact,
                Phone = respObj.ContactPhone,
                Province = respObj.Province,
                City = respObj.City,
                District = respObj.District,
                Address = respObj.Address
            };
        }

        public async Task<List<Region>> GetRegion(string tenantId)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }
            string url = $@"/api/administration/region/tree";
            var res = await httpClient.GetStringAsync(url);
            var resp = JsonConvert.DeserializeObject<ListDto<List<Region>>>(res);
            return resp.Items.Any() ?
                resp.Items.Select(r => new Region { Code = r.Code, Name = r.Name, Children = r.Children }).ToList() :
                new List<Region>();
        }

        public async Task<List<RouteScheme>> GetRouteScheme(string tenantId)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }
            string url = $@"/api/administration/routeScheme/all";
            var res = await httpClient.GetStringAsync(url);
            var resp = JsonConvert.DeserializeObject<ListDto<List<RouteSchemeDataDto>>>(res);
            return resp.Items.Any() ?
                resp.Items.Select(r => new RouteScheme
                {
                    Id = r.Id,
                    Name = r.Name,
                    Memo = r.Memo,
                    Items = r.Items.Select(i => new RouteSchemeItemData()).ToList(),
                    Locations = r.Locations.Select(l => new RouteSchemeLocation()).ToList()
                }).ToList() : new List<RouteScheme>();
        }

        public async Task<RouteScheme?> GetRouteSchemeById(string tenantId, string id)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }
            string url = $@"/api/administration/routeScheme/{id}";
            var res = await httpClient.GetStringAsync(url);
            var resp = JsonConvert.DeserializeObject<RouteSchemeDataDto>(res);
            return resp is not null ?
                new RouteScheme
                {
                    Id = resp.Id,
                    Name = resp.Name,
                    Memo = resp.Memo,
                    Items = resp.Items.Select(i => new RouteSchemeItemData()).ToList(),
                    Locations = resp.Locations.Select(l => new RouteSchemeLocation()).ToList()
                } : null;
        }

        public async Task<List<RouteScheme>> GetRouteScheme(string tenantId, string customerId)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }
            string url = $@"/api/administration/routeScheme/{customerId}/routeScheme";
            var res = await httpClient.GetStringAsync(url);
            var resp = JsonConvert.DeserializeObject<ListDto<List<RouteSchemeDataDto>>>(res);
            return resp.Items.Any() ?
                resp.Items.Select(r => new RouteScheme
                {
                    Id = r.Id,
                    Name = r.Name,
                    Memo = r.Memo,
                    Items = r.Items.Select(i => new RouteSchemeItemData()).ToList(),
                    Locations = r.Locations.Select(l => new RouteSchemeLocation()).ToList()
                }).ToList() : new List<RouteScheme>();
        }

        public async Task<DataResult<List<TransportPlanRet>>> GetTransPlan(string tenantId, OrderDeliveryInfo orderInfo)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }
            int customerId = 0;
            if (orderInfo.BusinessType == 3)
            {
                customerId = Convert.ToInt32(orderInfo.CustomerId);
            }
            else
            {
                var cargoOwnerRes = await httpClient.GetStringAsync("/api/consigner-management/consigner/all");
                var cargoOwnerList = JsonConvert.DeserializeObject<ListDto<List<ConsignerDto>>>(cargoOwnerRes);
                var cargoOwnerItem = cargoOwnerList.Items.Where(b => b.CustomerTripleId == orderInfo.CustomerId).FirstOrDefault();
                if (cargoOwnerItem != null)
                    customerId = cargoOwnerItem.CustomerTripleId;
            }
            if (customerId <= 0)
            {
                return new DataResult<List<TransportPlanRet>> { Message = "客户Id参数为空" };
            }
            var routeRes = await httpClient.GetStringAsync($@"/api/administration/routeScheme/{customerId}/routeScheme");
            var result = JsonConvert.DeserializeObject<ListDto<List<RouteSchemeDataDto>>>(routeRes);
            if (result is null || !result.Items.Any())
            {
                return new DataResult<List<TransportPlanRet>> { Message = "运输方案不存在" };
            }
            var allRouteScheme = result.Items;
            List<RouteSchemeDataDto> matchRouteScheme = new List<RouteSchemeDataDto>();
            //根据起始地、目的地过滤出符合条件的运输方案
            foreach (var item in allRouteScheme)
            {
                var startList = item.Locations.Where(_ => _.Mode == (int)ModeTypeEnum.Start).ToList();
                var endList = item.Locations.Where(_ => _.Mode == (int)ModeTypeEnum.End).ToList();
                foreach (var start in startList)
                {
                    var flag = true;
                    var startflag = false;
                    if (start == null)
                        continue;
                    if (start.Type == LocationTypeEnum.ByAddress.GetHashCode())
                    {
                        flag = flag && (start.LocationId == orderInfo.FromAddressId);
                    }
                    if (start.Type == LocationTypeEnum.ByCity.GetHashCode())
                    {
                        flag = flag && (start.Province == orderInfo.FromProvince) && (start.City == orderInfo.FromCity) && (start.District == orderInfo.FromArea);
                    }
                    if (flag)
                    {
                        startflag = true;
                        foreach (var end in endList)
                        {
                            if (end == null)
                                continue;
                            if (end.Type == LocationTypeEnum.ByAddress.GetHashCode())
                            {
                                flag = flag && (end.LocationId == orderInfo.ToAddressId);
                            }
                            if (end.Type == LocationTypeEnum.ByCity.GetHashCode())
                            {
                                flag = flag && (end.Province == orderInfo.ToProvince) && (end.City == orderInfo.ToCity) && (end.District == orderInfo.ToArea);
                            }

                            //符合条件的运输方案加入集合中
                            if (flag)
                            {
                                var locations = new List<RouteSchemeLocationDto>
                                    {
                                        start,
                                        end
                                    };
                                matchRouteScheme.Add(new RouteSchemeDataDto
                                {
                                    Id = item.Id,
                                    Name = item.Name,
                                    Memo = item.Memo,
                                    Items = item.Items,
                                    Locations = locations
                                });
                                break;
                            }
                        }
                    }
                    // 如果运输方案中的当前起始地符合，则跳出循环
                    if (startflag)
                    {
                        break;
                    }
                }

            }

            var list = new List<TransportPlanRet>();
            foreach (var item in matchRouteScheme)
            {
                var transportPlan = new TransportPlanRet()
                {
                    TransportPlanId = item.Id,
                    PlanName = item.Name,
                    Remark = item.Memo
                };
                var sort = 0;
                var transPlanDetails = new List<TransPlanDetail>();
                // 处理运力资源
                var carrierList = item.Items.Where(_ => _.Type == (int)CarrierTypeEnum.Transport).OrderBy(_ => _.Sort).ToList();
                foreach (var carrier in carrierList)
                {
                    sort++;
                    transPlanDetails.Add(new TransPlanDetail
                    {
                        Sort = sort,
                        TipKey = carrier.Key,
                        Tip = carrier.Name
                    });
                }
                // 处理起始地址
                var start = item.Locations.Where(_ => _.Mode == (int)ModeTypeEnum.Start).FirstOrDefault();
                if (start != null)
                {
                    var transPlanDetail = transPlanDetails.First();
                    if (start.Type == LocationTypeEnum.ByAddress.GetHashCode())
                    {
                        transPlanDetail.LabelKey = start.LocationId;
                        transPlanDetail.Label = start.LocationName;
                    }
                    if (start.Type == LocationTypeEnum.ByCity.GetHashCode())
                    {
                        transPlanDetail.LabelKey = "";
                        transPlanDetail.Label = start.Province + start.City + start.District;
                    }
                    transPlanDetails[0] = transPlanDetail;
                }
                // 处理中转地
                var netWorkList = item.Items.Where(_ => _.Type == (int)CarrierTypeEnum.NetWork).OrderBy(_ => _.Sort).ToList();
                for (int i = 0; i < netWorkList.Count; i++)
                {
                    transPlanDetails[i + 1].LabelKey = netWorkList[i].Key;
                    transPlanDetails[i + 1].Label = netWorkList[i].Name;
                }
                // 处理终点
                var end = item.Locations.Where(_ => _.Mode == (int)ModeTypeEnum.End).FirstOrDefault();
                if (end != null)
                {
                    var transPlanDetail = new TransPlanDetail();
                    if (end.Type == LocationTypeEnum.ByAddress.GetHashCode())
                    {
                        transPlanDetail.LabelKey = end.LocationId;
                        transPlanDetail.Label = end.LocationName;
                    }
                    if (end.Type == LocationTypeEnum.ByCity.GetHashCode())
                    {
                        transPlanDetail.LabelKey = "";
                        transPlanDetail.Label = end.Province + end.City + end.District;
                    }
                    sort++;
                    transPlanDetail.Sort = sort;
                    transPlanDetails.Add(transPlanDetail);
                }

                transportPlan.TransPlanDetails = transPlanDetails;
                list.Add(transportPlan);
            }
            return new DataResult<List<TransportPlanRet>> { Success = true, Data = list };
        }

        public async Task<List<WarehouseDescription>> GetWarehouse(string tenantId, int consignerId)
        {
            if (!string.IsNullOrEmpty(tenantId) && !httpClient.DefaultRequestHeaders.Contains("tenant"))
            {
                httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            }
            string url = $@"/api/warehouse-management/interfacewarehouse/consigner?ConsignerTripleId=" + consignerId;
            var res = await httpClient.GetStringAsync(url);
            var resp = JsonConvert.DeserializeObject<ListDto<List<SystemWarehouseDto>>>(res);
            return resp.Items.Any() ?
                resp.Items.Select(r => new WarehouseDescription(r.WarehouseId, r.InterfaceWarehouseId, r.WarehouseName)).ToList() :
                new List<WarehouseDescription>();
        }
    }
}
