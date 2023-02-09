using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.Application.Contracts.CollaborationServices.OrderCenter
{
    public interface IOrderCenterService
    {
        Task<IEnumerable<ProductDto>> QueryProduct(long cargoOwnerId, string barCode);
    }
}
