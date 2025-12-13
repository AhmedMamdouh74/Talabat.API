using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Order
{
    public class CreateOrderDto
    {
        public string BuyerEmail { get; set; }
        public int DeliveryMethodId { get; set; }
        public string BasketId { get; set; }
        public AddressDto ShipToAddress { get; set; }
    }
}
