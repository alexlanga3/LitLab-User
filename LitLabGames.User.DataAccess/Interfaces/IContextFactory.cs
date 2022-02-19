using LitLabGames.User.DataAccess.Context;

namespace LitLabGames.User.DataAccess.Interfaces
{
    public interface IContextFactory
    {
        LitLabContext GetContext();
    }
}
