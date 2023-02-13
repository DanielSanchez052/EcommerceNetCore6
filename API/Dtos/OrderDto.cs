using System.Reflection.Metadata.Ecma335;

namespace API.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int  DeliveryMethodId { get; set; }
        public AddressDto ShipToAddress { get; set; }
    }
}
