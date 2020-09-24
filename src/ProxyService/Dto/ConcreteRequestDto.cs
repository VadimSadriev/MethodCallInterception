using System.Collections.Generic;

namespace ProxyService.Dto
{
    public class ConcreteRequestDto
    {
        public string ConcreteString { get; set; }
        public long ConcreteLong { get; set; }
        public string[] ConcreteArray { get; set; }
        public List<ConcreteRequestItemDto> ConcreteList { get; set; }
        public ConcreteRequestItemDto ConcreteItem { get; set; }
    }
}
