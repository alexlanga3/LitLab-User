using LitLabGames.User.Crosscutting.Entities;
using LitLabGames.User.DataAccess.Context;
using LitLabGames.User.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LitLabGames.User.DataAccess.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
		/// The context factory (readonly).
		/// </summary>
		private readonly IContextFactory _contextFactory;

        /// <summary>
		/// The context.
		/// </summary>
		private readonly LitLabContext _context;

        public IContextFactory ContextFactory => _contextFactory;

        public GenericRepository(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _context = contextFactory.GetContext();
        }

        /// <summary>
		/// Get the db set.
		/// </summary>
		/// <returns>The <see cref="T:DbSet{TEntity}"/>.</returns>
		public DbSet<TEntity> GetDbSet()
        {
            return _context.Set<TEntity>();
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(TEntity entity)
        {
            _context.Entry<TEntity>(entity).State = EntityState.Deleted;
        }

        /// <summary>
		/// Get the by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns>The <see cref="TEntity"/>.</returns>
		public TEntity GetById(Guid id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Save the changes.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Save the changes async.
        /// </summary>
        /// <returns>The <see cref="T:Task{int}"/>.</returns>
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(TEntity entity)
        {
            _context.Update<TEntity>(entity);
        }
    }
}
