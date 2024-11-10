using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicQuery.Net.Dto.Input
{
     public class OrderFilterMetaDataInput:OrderFilterInput
    {
        public Dictionary<string,string> MetaData { get; set; }
    }
}
