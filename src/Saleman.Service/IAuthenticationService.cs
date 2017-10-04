using Saleman.Model.ServiceObjects;
using System.Threading.Tasks;

namespace Saleman.Service
{
    public interface IAuthenticationService : IService
    {
        Task<AuthenticationServiceObject> AuthenticateAsync(string userName, string password);
        Task<ResultServiceObject> VerifyEmailAsync(string id, string token);
        Task<TokenServiceObject> GenerateConfirmTokenAsync(string email);
        Task<ResultServiceObject> ResetPasswordAsync(ResetPasswordServiceObject request);
        Task SignoutAsync();
    }
}