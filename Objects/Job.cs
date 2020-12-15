using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSuiteByVesune.Objects
{
    public class Job
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public List<Charge> Charges { get; set; }
        public decimal BalanceDue { get; set; }
        public string City { get; set; }
        public bool Mobile { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int JobTypeId { get; set; }
        public string JobTypeName { get; set; }
        public int JobStatusTypeId { get; set; }
        public string JobStatusTypeName { get; set; }

    }
}
