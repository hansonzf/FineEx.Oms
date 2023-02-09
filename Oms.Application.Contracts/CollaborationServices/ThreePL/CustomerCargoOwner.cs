using Oms.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.Application.Contracts.CollaborationServices.ThreePL
{
    public class CustomerCargoOwner
    {
        public CustomerDescription Customer { get; set; }
        public List<CargoOwnerDescription> CargoOwners { get; set; }
    }
}
