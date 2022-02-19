using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LitLabGames.User.Domain.Interfaces
{
    public interface IUserDomainService
    {
        DataAccess.Entities.User GetUserByName(string name);
        void DeleteUserByName(DataAccess.Entities.User user);
        void UpdateUser(DataAccess.Entities.User user);
        Task<int> SaveChangesAsync();
        void AddUser(DataAccess.Entities.User user);
    }
}
