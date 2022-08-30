using FreeCourse.WebUI.Exceptions;
using FreeCourse.WebUI.Models;
using FreeCourse.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FreeCourse.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICatalogService _catalogService;
        public HomeController(ILogger<HomeController> logger,ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

       
        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCourseAsync());
        }

        [Route("/kurs/detay")]
        public async Task<IActionResult> Detail(string id)
        {
            return View(await _catalogService.GetByCourseId(id));
        }
      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorFeature = HttpContext.Features.Get<ExceptionHandlerFeature>();
            if (errorFeature!=null && errorFeature.Error is UnAuthorizedException)
            {
                return RedirectToAction("Logout", "Auth");
            }


            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
