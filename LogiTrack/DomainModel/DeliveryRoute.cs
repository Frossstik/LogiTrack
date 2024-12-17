using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrack.DomainModel
{
    public class DeliveryRoute
    {
        public string StartPoint { get; private set; }
        public string EndPoint { get; private set; }

        public DeliveryRoute(string startPoint, string endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }
}
