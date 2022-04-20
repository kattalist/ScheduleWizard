using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ScheduleWizard
{
    public class ClassTimeSlot
    {
        // A ClassTimeSlot contains a reference to the class it is a part of, as well as a day of the week, a start time, and an end time (in minutes since midnight)
        [XmlIgnore]
        public Class Parent;

        public Day Day;
        public int StartTime { get; set; }
        public int EndTime { get; set; }

        public ClassTimeSlot()
        {

        }
        public ClassTimeSlot(Class parent, Day day, int start, int end)
        {
            Parent = parent;
            Day = day;
            StartTime = start;
            EndTime = end;
        }
    }
}
