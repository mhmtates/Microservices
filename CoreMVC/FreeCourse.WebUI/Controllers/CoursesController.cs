using FreeCourse.Shared.Services;
using FreeCourse.WebUI.Models.Catalog;
using FreeCourse.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FreeCourse.WebUI.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        [Route("/kurslar")]
        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId)); ;
        }
        
        [HttpGet]
       public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories,"Id","Name");
            return View();
        }
       
        [HttpPost]
      
        public async Task<IActionResult> Create(CreateCourseInput createCourseInput)
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View();
            }
            createCourseInput.UserId = _sharedIdentityService.GetUserId;
            await _catalogService.CreateCourseAsync(createCourseInput);

            return RedirectToAction("Index","Courses");
        }

       
        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetByCourseId(id);
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name",course.Id);
            if(course==null)
            {
                return RedirectToAction("Index");
            }
            UpdateCourseInput updateCourseInput = new()
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                Feature = course.Feature,
                CategoryId = course.CategoryId,
                UserId = course.UserId,
                Picture = course.Picture
            };

            return View(updateCourseInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCourseInput updateCourseInput)
        {
            
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", updateCourseInput.Id);

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.UpdateCourseAsync(updateCourseInput);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCourseAsync(id);
            return RedirectToAction("Index");
        }

    }
}
