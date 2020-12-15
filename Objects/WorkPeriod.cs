using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSuiteByVesune.Objects
{
    public class WorkPeriod
    {
        public int WorkPeriodId { get; set; }
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string UserName { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public TimeSpan? WorkPeriodTime { get; set; }
        public string WorkPeriodReadable { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public int StartMinute { get; set; }
        public int EndMinute { get; set; }
        public int StartMeridiem { get; set; }
        public int EndMeridiem { get; set; }
    }
}
