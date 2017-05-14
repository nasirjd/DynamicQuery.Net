using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicQuery.Net.Dto.Input
{
     public class OrderFilterNonFilterInput
    {
        public List<OrderInput> Order { get; set; }
        public List<FilterInput> Filter { get; set; }
        public Dictionary<string,string> NonFilter { get; set; }
    }
}
