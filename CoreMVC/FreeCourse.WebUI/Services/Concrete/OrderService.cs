using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using FreeCourse.WebUI.Models.FakePayment;
using FreeCourse.WebUI.Models.Order;
using FreeCourse.WebUI.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.WebUI.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IPaymentService _paymentService;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderService(HttpClient httpClient, IPaymentService paymentService, IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _httpClient = httpClient;
            _paymentService = paymentService;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<CreatedOrderViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();
            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                Expiration = checkoutInfoInput.Expiration,
                CVV = checkoutInfoInput.CVV,
                TotalPrice = basket.TotalPrice,
            };
            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);
            if (!responsePayment)
            {
                return new CreatedOrderViewModel() { Error = "Ödeme alınamadı", IsSuccessful = false };
            }

            var orderInput = new CreateOrderInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new CreateAddressInput
                {
                    Province = checkoutInfoInput.Province,
                    District = checkoutInfoInput.District,
                    Street = checkoutInfoInput.Street,
                    ZipCode = checkoutInfoInput.ZipCode,
                    Line = checkoutInfoInput.Line
                },
            };

            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new CreateOrderItemInput { ProductId = x.CourseId, Price = x.GetCurrentPrice, PictureUrl = "", ProductName = x.CourseName };
                orderInput.OrderItems.Add(orderItem);
            });

            var response = await _httpClient.PostAsJsonAsync("orders", orderInput);
            if (!response.IsSuccessStatusCode)
            {
                return new CreatedOrderViewModel(){Error = "Sipariş oluşturulamadı", IsSuccessful = false };
            }
          
            var createdOrder= await response.Content.ReadFromJsonAsync<Response<CreatedOrderViewModel>>();
            createdOrder.Data.IsSuccessful = true;
            await _basketService.Delete();
            return createdOrder.Data;
          
        }
        public async Task<List<OrderViewModel>> GetOrders()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");
            return response.Data;
        }

        public async Task<SuspendOrderViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();
            var orderInput = new CreateOrderInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new CreateAddressInput
                {
                    Province = checkoutInfoInput.Province,
                    District = checkoutInfoInput.District,
                    Street = checkoutInfoInput.Street,
                    ZipCode = checkoutInfoInput.ZipCode,
                    Line = checkoutInfoInput.Line
                },
            };

            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new CreateOrderItemInput { ProductId = x.CourseId, Price = x.GetCurrentPrice, PictureUrl = "", ProductName = x.CourseName };
                orderInput.OrderItems.Add(orderItem);
            });

            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                Expiration = checkoutInfoInput.Expiration,
                CVV = checkoutInfoInput.CVV,
                TotalPrice = basket.TotalPrice,
                Order = orderInput
            };

            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);
            if (!responsePayment)
            {
                return new SuspendOrderViewModel() { Error = "Ödeme alınamadı", IsSuccessful = false };
            }
            await _basketService.Delete();
            return new SuspendOrderViewModel() { IsSuccessful = true };

        }
    }
}
