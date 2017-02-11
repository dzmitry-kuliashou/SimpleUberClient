using WebApiCaller.Common;

namespace WebApiCaller.Services.Authorization
{
    public interface IAuthorizationService
    {
        ServiceResponse<string> Authorize();
    }
}
