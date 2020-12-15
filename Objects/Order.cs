using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSuiteByVesune.Objects
{
    public class Order
    {
        //TODO: attach orders to a job
        public int OrderId { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime? OrderedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }

    }
}
