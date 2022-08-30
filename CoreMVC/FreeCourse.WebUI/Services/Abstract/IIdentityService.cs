using FreeCourse.Shared.Dtos;
using FreeCourse.WebUI.Models.User;
using IdentityModel.Client;
using System.Threading.Tasks;

namespace FreeCourse.WebUI.Services.Abstract
{
  public interface IIdentityService 
    {
        Task<Response<bool>> SignIn(UserInput userinput);

        Task<TokenResponse> GetAccessTokenByRefreshToken();

        Task RevokeRefreshToken(); //Kullanıcı sistemden çıkış yaptığında refresh token'ı veritabanından sil.
    }
}
