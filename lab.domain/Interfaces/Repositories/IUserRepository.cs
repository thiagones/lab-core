using System.Threading.Tasks;
using lab.domain.Models.Data;

namespace lab.domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<UserDataModel>
    {
        Task<UserDataModel> AuthenticateAsync(string username, string password);   
    }
}