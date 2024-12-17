using LogiTrack.DomainModel;
using System;
using System.Collections.Generic;
using LogiTrack.ActiveRecord;
using LogiTrack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogiTrack.TransactionSript
{
    
    public class ScheduleTransactionScript
    {
        private readonly DriverRepository _driverRepository;

        public ScheduleTransactionScript(DriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public void AddShift(Guid driverId, Shift shift)
        {
            var driver = _driverRepository.FindById(driverId);
            if (driver == null)
                throw new Exception("Водитель не найден.");

            if (driver.Shifts.Any(existingShift => existingShift.OverlapsWith(shift)))
                throw new Exception("Новая смена конфликтует с уже существующей.");

            driver.Shifts.Add(shift);
            _driverRepository.Save(driver);
        }

        public List<Shift> GetShiftsForDate(Guid driverId, DateTime date)
        {
            var driver = _driverRepository.FindById(driverId);
            if (driver == null)
                throw new Exception("Водитель не найден.");

            return driver.Shifts.Where(shift => shift.Date.Date == date.Date).ToList();
        }

        public bool IsDriverAvailable(Guid driverId, DateTime dateTime)
        {
            var driver = _driverRepository.FindById(driverId);
            if (driver == null)
                throw new Exception("Водитель не найден.");

            return driver.Shifts.All(shift => !shift.Contains(dateTime));
        }
    }
}
