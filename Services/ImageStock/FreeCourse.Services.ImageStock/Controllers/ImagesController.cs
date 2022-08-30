using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using FreeCourse.Services.ImageStock.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Services.ImageStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> ImageSave(IFormFile image, CancellationToken cancellationToken)
        {
            if (image != null && image.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                await image.CopyToAsync(stream, cancellationToken);

                var returnPath = image.FileName;

                ImageDto imageDto = new() { Url = returnPath };

                return CreateActionResultInstance(Response<ImageDto>.Success(imageDto, 200));
            }

            return CreateActionResultInstance(Response<ImageDto>.Fail("Image is empty", 400));
        }


        [HttpDelete]
        public IActionResult ImageDelete(string imageUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("Image is not found", 404));
            }

            System.IO.File.Delete(path);

            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}