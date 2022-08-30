using FreeCourse.WebUI.Models.Settings;
using Microsoft.Extensions.Options;


namespace FreeCourse.WebUI.Helpers
{
    public class ImageHelper
    {
        private readonly ServiceApiSettings _serviceApiSettings;

        public ImageHelper(IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public string GetImageStockUrl (string imageUrl)
        {
            return $"{_serviceApiSettings.ImageStockUri}/images/{imageUrl}";
        }

        
    }
}
