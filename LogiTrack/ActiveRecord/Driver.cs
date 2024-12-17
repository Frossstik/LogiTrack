using System;
using System.Collections.Generic;
using System.Linq;

namespace LogiTrack.ActiveRecord
{
    public class Driver
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<Shift> Shifts { get; private set; } = new();

        public Driver(Guid id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public bool HasConflictingShift(Shift newShift)
        {
            return Shifts.Any(existingShift => existingShift.ConflictsWith(newShift));
        }

        public void AddShift(Shift newShift)
        {
            if (HasConflictingShift(newShift))
                throw new InvalidOperationException("Новая смена конфликтует с уже существующей.");

            Shifts.Add(newShift);
        }

        public IEnumerable<Shift> GetShiftsForDate(DateTime date)
        {
            return Shifts.Where(shift => shift.Date == date.Date);
        }

        public bool IsAvailable(DateTime dateTime)
        {
            return Shifts.All(shift => !shift.Contains(dateTime));
        }
    }
}
