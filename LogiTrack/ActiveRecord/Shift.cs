using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace LogiTrack.ActiveRecord
{
    public class Shift
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public Shift(DateTime startTime, DateTime endTime)
        {
            if (endTime <= startTime)
                throw new ArgumentException("Время окончания маршрута должно быть позже времени начала.");

            StartTime = startTime;
            EndTime = endTime;
        }

        public DateTime Date => StartTime.Date;

        public bool OverlapsWith(Shift other)
        {
            return StartTime < other.EndTime && EndTime > other.StartTime;
        }

        public bool ConflictsWith(Shift other)
        {
            return StartTime < other.EndTime && EndTime > other.StartTime;
        }

        public bool Contains(DateTime dateTime)
        {
            return dateTime >= StartTime && dateTime <= EndTime;
        }
    }
}

