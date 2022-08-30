using FreeCourse.Shared.Dtos;
using FreeCourse.WebUI.Helpers;
using FreeCourse.WebUI.Models.Catalog;
using FreeCourse.WebUI.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;
        private readonly IImageStockService _imageStockService;
        private readonly ImageHelper _imageHelper;

        public CatalogService(HttpClient client, IImageStockService imageStockService, ImageHelper imageHelper)
        {
            _client = client;
            _imageStockService = imageStockService;
            _imageHelper = imageHelper;
        }

        public async Task<bool> CreateCourseAsync(CreateCourseInput createCourseInput)
        {
            var resultImageService = await _imageStockService.UploadImage(createCourseInput.ImageFormFile);

            if (resultImageService != null)
            {
                createCourseInput.Picture = resultImageService.Url;
            }

            var response = await _client.PostAsJsonAsync<CreateCourseInput>("courses", createCourseInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _client.DeleteAsync($"courses/{courseId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _client.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            //http:localhost:5000/services/catalog/courses
            var response = await _client.GetAsync("courses");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            responseSuccess.Data.ForEach(x =>
            {
                x.StockPictureUrl = _imageHelper.GetImageStockUrl(x.Picture);
            });
            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            //[controller]/GetAllByUserId/{userId}

            var response = await _client.GetAsync($"courses/GetAllByUserId/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            responseSuccess.Data.ForEach(x =>
            {
                x.StockPictureUrl = _imageHelper.GetImageStockUrl(x.Picture);
            });

            return responseSuccess.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            var response = await _client.GetAsync($"courses/{courseId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            responseSuccess.Data.StockPictureUrl = _imageHelper.GetImageStockUrl(responseSuccess.Data.Picture);

            return responseSuccess.Data;
        }

        public async Task<bool> UpdateCourseAsync(UpdateCourseInput updateCourseInput)
        {
            var resultImageService = await _imageStockService.UploadImage(updateCourseInput.ImageFormFile);

            if (resultImageService != null)
            {
                await _imageStockService.DeleteImage(updateCourseInput.Picture);
                updateCourseInput.Picture = resultImageService.Url;
            }

            var response = await _client.PutAsJsonAsync<UpdateCourseInput>("courses", updateCourseInput);

            return response.IsSuccessStatusCode;
        }
    }
}