using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LitLabGames.User.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Entities.User GetUserByName(string name);
        Task<int> SaveChangesAsync();
        void Add(Entities.User user);
        void Delete(Entities.User entity);
        void Update(Entities.User entity);
    }
}
