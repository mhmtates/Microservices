using FreeCourse.WebUI.Models.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.WebUI.Services.Abstract
{
  public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCourseAsync();

        Task<List<CategoryViewModel>> GetAllCategoryAsync();
        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);

        Task<CourseViewModel> GetByCourseId(string courseId);


        Task<bool> CreateCourseAsync(CreateCourseInput CreateCourseInput);


        Task<bool> UpdateCourseAsync(UpdateCourseInput UpdateCourseInput);
        
        
        Task<bool> DeleteCourseAsync(string courseId);



    }
}
