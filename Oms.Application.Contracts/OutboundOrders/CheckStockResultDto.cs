namespace Oms.Application.Orders
{
    public class CheckstockResultDto
    {
        public int BusinessType { get; set; }

        public List<CheckedListDto> PassedList { get; set; }

        public List<StockOutListDto> NotPassedList { get; set; }
    }

    public class CheckedListDto
    {
        public long OutBoundID { get; set; }
        public List<CheckedDetailListDto> DetailList { get; set; }
    }

    public class CheckedDetailListDto
    {
        public long OrderDetailID { get; set; }
        public long ProductBatchID { get; set; }
        public string ProductBatch { get; set; }
        public int CommodityID { get; set; }
        public int MemberID { get; set; }
        public int StockType { get; set; }
        public int PickAmount { get; set; }
        public int IsSpecifiedBatch { get; set; }
    }

    public class StockOutListDto
    {
        public long OutBoundID { get; set; }
        public List<StockOutDetailListDto> DetailList { get; set; }
    }

    public class StockOutDetailListDto
    {
        public long OrderDetailID { get; set; }
    }
}
