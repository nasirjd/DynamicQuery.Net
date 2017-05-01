using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicQuery.Net.Dto.Input
{
    public class OrderFilterInput
    {
        public FilterInput[] Filter { get; set; }
        public OrderInput[] Order { get; set; }
    }
}
