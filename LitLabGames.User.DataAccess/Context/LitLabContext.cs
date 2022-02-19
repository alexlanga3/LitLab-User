using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace LitLabGames.User.DataAccess.Context
{
    public class LitLabContext : DbContext
    {
		/// <summary>
		/// The current transaction.
		/// </summary>
		private IDbContextTransaction _currentTransaction = null;

		public LitLabContext(DbContextOptions<LitLabContext> options)
            : base(options)
        {
        }

        public LitLabContext() : base()
        {

        }

        /// <summary>
		/// Gets or sets the Users.
		/// </summary>
        public DbSet<Entities.User> Users { get; set; }

		/// <summary>
		/// Rollback the transaction.
		/// </summary>
		/// <exception cref="TransactionException">There are not opened transaction.</exception>
		public void RollbackTransaction()
		{
			if (_currentTransaction == null)
			{
				throw new Exception("There are not opened transaction.");
			}

			_currentTransaction.Rollback();
			_currentTransaction.Dispose();
			_currentTransaction = null;
		}
	}
}
