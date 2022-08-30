using FreeCourse.WebUI.Models.Order;
using FreeCourse.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FreeCourse.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }


        //[Route("/odeme")]
        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();
            ViewBag.basket = basket;
            return View(new CheckoutInfoInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
        {
            // Senkron iletişim
            /*  var orderStatus = await _orderService.CreateOrder(checkoutInfoInput)*/
            ;
            // Asenkron iletişim
            var orderSuspend = await _orderService.SuspendOrder(checkoutInfoInput);
            if (!orderSuspend.IsSuccessful)
            {
                var basket = await _basketService.Get();
                ViewBag.basket = basket;
                ViewBag.error = orderSuspend.Error;
                return View();
            }
            // Senkron iletişim
            //   return RedirectToAction("SuccessfulCheckout",new{orderId = orderStatus.OrderId });
            // Asenkron iletişim
            return RedirectToAction("SuccessfulCheckout", new { orderId = new Random().Next(1, 1000) });
        }


        //[Route("/odeme-basarili")]
        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }

        public async Task<IActionResult> CheckoutHistory()
        {
            return View(await _orderService.GetOrders());
        }
    }
}
