using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrack.DomainModel
{
    public class FraudDetectionService
    {
        public bool DetectFraud(TransportationOrder order)
        {
            var recentMessages = order.Messages.Where(m => m.Timestamp > DateTime.UtcNow - TimeSpan.FromMinutes(10));
            return recentMessages.Count() > 10;
        }
    }

}
