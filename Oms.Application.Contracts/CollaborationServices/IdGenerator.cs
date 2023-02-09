using FCL.IdWorker;

namespace Oms.Application.Contracts.CollaborationServices
{
    public class IdGenerator
    {
        static readonly string IDWORKADDR = "172.16.100.11:10002";

        public static long GetId()
        {
            IdWorkerProxy proxy = new IdWorkerProxy(IDWORKADDR);
            return proxy.NewID();
        }

        public static List<long> GetId(int count)
        {
            IdWorkerProxy proxy = new IdWorkerProxy(IDWORKADDR);
            return proxy.ListNewID(count);
        }
    }
}
