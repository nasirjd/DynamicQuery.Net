using System.Collections.Generic;

namespace DynamicQuery.Net.Dto.Input
{
   public class DynamicQueryNetInput
    {
        public List<FilterInput> Filter { get; set; }
        public List<OrderInput> Order { get; set; }
        public Dictionary<string,string> NonFilter { get; set; }
        public Paging Paging { get; set; }
    }
}
