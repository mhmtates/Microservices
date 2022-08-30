using FreeCourse.Shared.Dtos;
using FreeCourse.WebUI.Models.ImageStock;
using FreeCourse.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class ImageStockService : IImageStockService
    {
        private readonly HttpClient _httpClient;

        public ImageStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeleteImage(string imageUrl)
        {
            var response = await _httpClient.DeleteAsync($"Images?ImageUrl=imageUrl");
            return response.IsSuccessStatusCode;
        }

        public async Task<ImageViewModel> UploadImage(IFormFile image)
        {
            if (image == null || image.Length <= 0)
            {
                return null;
            }
            // örnek dosya ismi= 203802340234.jpg
            var randomFilename = $"{Guid.NewGuid().ToString()}{Path.GetExtension(image.FileName)}";

            using var ms = new MemoryStream();

            await image.CopyToAsync(ms);

            var multipartContent = new MultipartFormDataContent();

            multipartContent.Add(new ByteArrayContent(ms.ToArray()), "image", randomFilename);

            var response = await _httpClient.PostAsync("images", multipartContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<ImageViewModel>>();

            return responseSuccess.Data;
        }

      
    }
}