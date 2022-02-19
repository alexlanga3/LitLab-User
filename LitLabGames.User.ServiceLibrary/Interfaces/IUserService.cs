using LitLabGames.User.ServiceLibrary.DTOs;
using System.Threading.Tasks;

namespace LitLabGames.User.ServiceLibrary.Interfaces
{
    public interface IUserService
    {
        UserDTO GetUserByName(string name);
        Task<bool> SaveAsync(UserDTO userDTO);
        bool DoExtraValidationOnUser(UserDTO userDTO);
        Task<bool> DeleteAsync(string name);
        Task<bool> UpdateAsync(UserDTO userDTO);
    }
}
