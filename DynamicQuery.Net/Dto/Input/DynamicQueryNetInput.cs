using System.Collections.Generic;

namespace DynamicQuery.Net.Dto.Input
{
   public class DynamicQueryNetInput:OrderFilterMetaDataInput
    {
        public PagingInput Pagination { get; set; }
    }
}
