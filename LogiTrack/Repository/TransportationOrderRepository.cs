using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogiTrack.DomainModel;

namespace LogiTrack.Repository
{
    public class TransportationOrderRepository : IRepository<TransportationOrder>
    {
        private readonly List<TransportationOrder> _orders = new();

        public void Add(TransportationOrder entity) => _orders.Add(entity);

        public void Delete(Guid id) => _orders.RemoveAll(o => o.Id == id);

        public IEnumerable<TransportationOrder> GetAll() => _orders;

        public TransportationOrder GetById(Guid id) => _orders.FirstOrDefault(o => o.Id == id);

        public void Update(TransportationOrder entity)
        {
            var index = _orders.FindIndex(o => o.Id == entity.Id);
            if (index >= 0) _orders[index] = entity;
        }
    }

}
