using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSuiteByVesune.Objects
{
    public class Master // by job
    {
        public int JobId { get; set; }
        public int UserId { get; set; }
        public string JobName { get; set; }

        public TimeSpan TotalJobTime { get; set; }
        public string TotalJobTimeReadable { get; set; }

        public decimal AmoutPaidForJob { get; set; }
        public string AmountPaidForJobReadable { get; set; }

        public decimal HoursPaid { get; set; }
        public int HoursOwed { get; set; }


        public decimal MoneyPaid { get; set; }
        public decimal MoneyOwed { get; set; }
    }
}
