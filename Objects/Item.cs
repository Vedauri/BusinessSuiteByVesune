using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSuiteByVesune.Objects
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int LocationTypeId { get; set; }
        public int ItemTypeId { get; set; }
        public string SkuNumber { get; set; }
        public string LocationTypeName { get; set; }
        public string ItemTypeName { get; set; }
    }
}
