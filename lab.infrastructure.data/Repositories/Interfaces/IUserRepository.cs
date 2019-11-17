using System.Collections.Generic;
using System.Threading.Tasks;
using lab.infrastructure.data.Models;

namespace lab.infrastructure.data.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<UserDataModel>
    {
        Task<UserDataModel> AuthenticateAsync(string username, string password);   
    }
}