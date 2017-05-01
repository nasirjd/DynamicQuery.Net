using DynamicQuery.Net.Enums;

namespace DynamicQuery.Net.Dto.Input
{
    public class OrderInput
    {
        public string Property { get; set; }
        public OrderTypeEnum Order { get; set; }
    }
}