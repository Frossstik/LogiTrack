using LogiTrack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrack.DomainModel
{
    public class OrderLifecycleService
    {
        private readonly IRepository<TransportationOrder> _repository;

        public OrderLifecycleService(IRepository<TransportationOrder> repository)
        {
            _repository = repository;
        }

        public void CloseInactiveOrders(TimeSpan inactivityThreshold)
        {
            var orders = _repository.GetAll();
            foreach (var order in orders)
            {
                if (order.IsInactive(inactivityThreshold))
                {
                    order.CloseOrder();
                    _repository.Update(order);
                }
            }
        }
    }

}
