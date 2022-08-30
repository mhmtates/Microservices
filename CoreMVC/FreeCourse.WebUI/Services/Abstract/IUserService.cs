using FreeCourse.WebUI.Models.User;
using System.Threading.Tasks;

namespace FreeCourse.WebUI.Services.Abstract
{
   public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
