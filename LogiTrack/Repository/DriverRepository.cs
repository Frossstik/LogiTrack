using LogiTrack.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrack.Repository
{
    public class DriverRepository
    {
        private readonly Dictionary<Guid, Driver> _drivers = new();

        public Driver? FindById(Guid driverId)
        {
            return _drivers.TryGetValue(driverId, out var driver) ? driver : null;
        }

        public void Save(Driver driver)
        {
            _drivers[driver.Id] = driver;
        }

        public void Delete(Guid driverId)
        {
            _drivers.Remove(driverId);
        }
    }
}

