using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Client
{
    public class CommodityQueryResponse
    {
        public bool Flag { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// 明细数据
        /// </summary>
        public List<CommodityModel> Data { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 明细实体
        /// </summary>
        public class CommodityModel
        {
            /// <summary>
            /// 商品ID
            /// </summary>
            public int CommodityID { get; set; }

            /// <summary>
            /// 条码
            /// </summary>
            public string BarCode { get; set; }

            /// <summary>
            /// 商品名称
            /// </summary>
            public string CommodityName { get; set; }

            /// <summary>
            /// 编码
            /// </summary>
            public string CommodityCode { get; set; }

            /// <summary>
            /// 规则
            /// </summary>
            public string Stand { get; set; }

            /// <summary>
            /// 交互ID
            /// </summary>
            public string ItemID { get; set; }

            /// <summary>
            /// 是否组合商品
            /// </summary>
            public int UnitFlag { get; set; }
        }
    }
}
