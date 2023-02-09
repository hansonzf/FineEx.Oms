namespace Oms.Application.Contracts.CollaborationServices.Tms
{
    public class IssueToTmsResult
    {
        public bool IsSuccess { get; set; }
        public string UpOrderCode { get; set; }
        public string TransOrderCode { get; set; }
    }
}
