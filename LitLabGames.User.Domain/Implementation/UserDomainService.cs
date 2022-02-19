using LitLabGames.User.DataAccess.Interfaces;
using LitLabGames.User.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LitLabGames.User.Domain.Implementation
{
    public class UserDomainService : IUserDomainService
    {

        /// <summary>
		/// The repository (readonly).
		/// </summary>
		private readonly IUserRepository _userRepository;

        public UserDomainService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _userRepository.SaveChangesAsync();
        }

        public void DeleteUserByName(DataAccess.Entities.User user)
        {  
            _userRepository.Delete(user);         
        }

        public DataAccess.Entities.User GetUserByName(string name)
        {
            return _userRepository.GetUserByName(name);          
        }

        public void AddUser(DataAccess.Entities.User user)
        {
            _userRepository.Add(user);              
        }

        public void UpdateUser(DataAccess.Entities.User user)
        {       
            _userRepository.Update(user);         
        }
    }
}
