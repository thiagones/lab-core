using System.Linq;
using System.Threading.Tasks;
using lab.domain.Interfaces.Repositories;
using lab.domain.Models.Data;
using lab.infrastructure.data.Database.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace lab.infrastructure.data.Repositories
{
    public class UserRepository : BaseRepository<UserDataModel>, IUserRepository
    {
        private readonly IMongoContext _context;

        public UserRepository(IMongoContext mongoContext)
        : base(mongoContext)
        {
            _context = mongoContext;
        }

        public async Task<UserDataModel> AuthenticateAsync(string username, string password)
        {
            var user = await _context.GetCollection<UserDataModel>()
                 .AsQueryable()
                 .FirstOrDefaultAsync(
                     x =>
                         x.Username == username &&
                         x.Password == password
                 );

            return user;
        }
    }
}