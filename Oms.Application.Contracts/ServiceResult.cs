namespace Oms.Application.Contracts
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Error { get; set; }
    }

    public class DataResult<T> : ServiceResult
    {
        public T? Data { get; set; }
    }

    public class ListResult<T> : ServiceResult
    {
        public IEnumerable<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
