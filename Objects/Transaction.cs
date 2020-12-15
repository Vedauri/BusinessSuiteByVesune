using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSuiteByVesune.Objects
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public int JobId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string JobName { get; set; }
    }
}
