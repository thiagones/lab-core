using System.Collections.Generic;
using System.Threading.Tasks;
using lab.domain.Models;

namespace lab.service.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> AddAsync(UserModel user);
        Task<UserModel> AuthenticateAsync(string username, string password);
    }
}