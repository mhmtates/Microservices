using FreeCourse.WebUI.Models.Basket;
using FreeCourse.WebUI.Models.Discount;
using FreeCourse.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.WebUI.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public BasketController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        [Route("/sepetim")]
        public async Task<IActionResult> Index()
        {
            return View(await _basketService.Get());
        }

        
        public async Task<IActionResult> AddBasketItem(string courseId)
        {
            var course = await _catalogService.GetByCourseId(courseId);
            var basketItem = new BasketItemViewModel { CourseId = course.Id, CourseName = course.Name, Price = course.Price };
            await _basketService.AddBasketItem(basketItem);
            return RedirectToAction("Index");
        }

       
        public async Task<IActionResult> RemoveBasketItem(string courseId)
        {
            await _basketService.RemoveBasketItem(courseId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ApplyDiscount(DiscountInput discountInput)
        {
            if (!ModelState.IsValid)
            {
                TempData["discountError"] = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).First();
                return RedirectToAction("Index");
            }
            var discountStatus = await _basketService.ApplyDiscount(discountInput.Code);

            TempData["discountStatus"] = discountStatus;

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CancelAppliedDiscount()
        {
            await _basketService.CancelAppliedDiscount();
            return RedirectToAction("Index");
        }

    }
}
