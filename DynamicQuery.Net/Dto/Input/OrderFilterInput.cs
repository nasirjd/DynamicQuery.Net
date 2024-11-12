using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicQuery.Net.Dto.Input
{
    public class OrderFilterInput
    {
        public IEnumerable<FilterInput> GlobalPropertyFilters { get; set; }
        public IEnumerable<FilterInput> PropertyFilters { get; set; }
        public IEnumerable<OrderInput> Orders { get; set; }
    }
}
