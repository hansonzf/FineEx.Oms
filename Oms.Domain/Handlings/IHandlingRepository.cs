using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.Domain.Handlings
{
    public interface IHandlingRepository
    {
        Task InsertAsync(Handling log);
    }
}
