using Saleman.Model.ServiceObjects;
using System.Threading.Tasks;

namespace Saleman.Service
{

    public interface IUserService : IService
    {
        Task<UserServiceObject> CreateUserAsync(string email, string password);

        Task<UserServiceObject> GetUserByUserNameAsync(string userName);
    }
}