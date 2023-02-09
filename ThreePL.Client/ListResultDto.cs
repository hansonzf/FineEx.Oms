using System;
using System.Collections.Generic;

namespace Oms.Application.Contracts.CollaborationServices.ThreePL
{
    [Serializable]
    public class ListDto<T>
    {
        public T Items;
    }
}