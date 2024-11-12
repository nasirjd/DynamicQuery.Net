using System.Collections.Generic;
using DynamicQuery.Net.Enums;

namespace DynamicQuery.Net.Dto.Input
{
   public class DynamicQueryNetInput:OrderFilterMetaDataInput
    {
        public PaginationInput Pagination { get; set; }
    }
}
