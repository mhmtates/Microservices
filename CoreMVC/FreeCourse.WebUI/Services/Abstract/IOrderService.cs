using FreeCourse.WebUI.Models.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.WebUI.Services.Abstract
{
  public interface IOrderService
    {
        Task<CreatedOrderViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);

        Task<SuspendOrderViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput);

        Task<List<OrderViewModel>> GetOrders();
    }
}
