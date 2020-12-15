using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSuiteByVesune.Objects
{
    public class User
    {
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Pin { get; set; }
        public int WorkPeriodId { get; set; }
        public TimeSpan WeekHours { get; set; }
        public string WeekHoursReadable { get; set; }
        public bool Working { get; set; }
        public decimal WorkRate { get; set; }
    }
}
