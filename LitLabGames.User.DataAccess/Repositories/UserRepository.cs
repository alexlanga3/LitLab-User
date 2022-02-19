using LitLabGames.User.DataAccess.Interfaces;
using System.Linq;

namespace LitLabGames.User.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<Entities.User>, IUserRepository
    {
        public UserRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        
        }

        public Entities.User GetUserByName(string name)
        => GetDbSet().FirstOrDefault(b => b.Name == name);
    }
}
