using LitLabGames.User.DataAccess.Interfaces;
using System;

namespace LitLabGames.User.DataAccess.Context
{
    public class ContextFactory : IContextFactory, IDisposable
    {
        /// <summary>
        /// The context.
        /// </summary>
        private LitLabContext _context;


        public ContextFactory(LitLabContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the context.
        /// </summary>
        /// <returns>The <see cref="LitLabContext"/>.</returns>
        public LitLabContext GetContext()
        {
            if (_context == null)
            {
                _context = new LitLabContext();
            }
            return _context;
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
