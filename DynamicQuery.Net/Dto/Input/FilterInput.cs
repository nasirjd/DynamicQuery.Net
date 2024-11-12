using DynamicQuery.Net.Enums;
using DynamicQuery.Net.Services;

namespace DynamicQuery.Net.Dto.Input
{
    public class FilterInput
    {
        public string Property { get; set; }
        public object Value { get; set; }
        public OperationTypeEnum Operation { get; set; }
        public InputTypeEnum Type { get; set; }
    }
}
