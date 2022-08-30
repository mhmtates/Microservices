using System.Threading.Tasks;

namespace FreeCourse.WebUI.Services.Abstract
{
   public interface IClientCredentialTokenService
    {
        Task<string> GetToken();
    }
}
