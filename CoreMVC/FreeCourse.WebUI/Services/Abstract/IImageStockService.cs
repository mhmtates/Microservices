using FreeCourse.WebUI.Models.ImageStock;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FreeCourse.WebUI.Services.Abstract
{
   public interface IImageStockService
    {
        Task<ImageViewModel> UploadImage(IFormFile image);

        Task<bool> DeleteImage(string imageUrl);
    }
}
