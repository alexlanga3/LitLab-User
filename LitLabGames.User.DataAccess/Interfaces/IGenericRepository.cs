using LitLabGames.User.Crosscutting.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LitLabGames.User.DataAccess.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        DbSet<TEntity> GetDbSet();
        void Add(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetById(Guid id);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Update(TEntity entity);
    }
}
