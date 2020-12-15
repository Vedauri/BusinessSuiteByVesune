using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSuiteByVesune.Objects
{
    public class Charge
    {
        public int ChargeId { get; set; }
        public int JobId { get; set; }
        public string ChargeName { get; set; }
        public decimal ChargeAmount { get; set; }
        public int ChargeQuantity { get; set; }
        public string ChargeDescription { get; set; }
        public string ChargeNotes { get; set; }
    }
}
