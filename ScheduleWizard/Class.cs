using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleWizard
{
    public class Class
    {
        // A class object contains a code, a name, a location, and a list of ClassTimeSlot objects.
        public string Code { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public List<ClassTimeSlot> TimeSlots = new List<ClassTimeSlot>();

        public Class()
        {

        }
        public Class(string code, string name, string loc)
        {
            Code = code;
            Name = name;
            Location = loc;
        }

        public void AddTimeSlot(Day day, int start, int end)
        {
            TimeSlots.Add(new ClassTimeSlot(this, day, start, end));
        }

        public void AddTimeSlot(int dayNum, int start, int end)
        {
            TimeSlots.Add(new ClassTimeSlot(this, (Day)dayNum, start, end));
        }
    }
}
