using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleWizard
{
    public class Deadline
    {
        // A deadline contains the name of what is due, the class the deadline is for, and the due date and time.
        public string Name { get; set; }
        public Class Parent;
        public DateTime DueDate { get; set; }

        public Deadline(string name, Class parent, DateTime date)
        {
            Name = name;
            Parent = parent;
            DueDate = date;
        }

        public TimeSpan TimeRemaining(DateTime current)
        {
            return DueDate - current;
        }
    }
}
