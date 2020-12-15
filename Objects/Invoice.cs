using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSuiteByVesune.Objects
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string InvoiceName { get; set; }
        public int InvoiceTypeId { get; set; }
        public string InvoiceTypeName { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
    }
}
