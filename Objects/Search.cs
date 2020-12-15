using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSuiteByVesune.Objects
{
    public class Search
    {
        public int SearchId { get; set; }
        public Item SearchItem { get; set; }
        public Order SearchOrder { get; set; }
        public Job SearchJob { get; set; }
        public User SearchUser { get; set; }
        public Invoice SearchInvoice { get; set; }
        public WorkPeriod SearchWorkPeriod { get; set; }
    }
}
