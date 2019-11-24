using System.Threading.Tasks;
using lab.domain.Models;

namespace lab.domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserModel> AddAsync(UserModel user);
        Task<UserModel> AuthenticateAsync(string username, string password);
    }
}