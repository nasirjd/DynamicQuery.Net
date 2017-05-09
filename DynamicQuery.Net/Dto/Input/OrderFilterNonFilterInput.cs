using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicQuery.Net.Dto.Input
{
     public class OrderFilterNonFilterInput
    {
        public OrderInput[] Order { get; set; }
        public FilterInput[] Filter { get; set; }
        public Dictionary<string,object> NonFilter { get; set; }
    }
}
