using System.Collections.Generic;

namespace FreeCourse.WebUI.Models.Order
{
    public class CreateOrderInput
    {
        public CreateOrderInput()
        {
            OrderItems = new List<CreateOrderItemInput>();
        }
        public string BuyerId { get; set; }

        public List<CreateOrderItemInput> OrderItems { get; set; }

        public CreateAddressInput Address { get; set; }
    }
}
