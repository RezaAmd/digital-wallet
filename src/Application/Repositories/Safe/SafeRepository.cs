using DigitalWallet.Application.Extensions;
using DigitalWallet.Application.Interfaces.Context;
using DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities;
using System.Data.Entity;

namespace DigitalWallet.Application.Repositories.Safe
{
    public class SafeRepository : ISafeRepository
    {
        #region Initialize
        private readonly IDbContext context;
        public SafeRepository(IDbContext _context)
        {
            context = _context;
        }
        #endregion

        /// <summary>
        /// Create a new safe for user.
        /// </summary>
        /// <param name="safe">New safe model object.</param>
        public async Task<Result> CreateAsync(SafeEntity safe, CancellationToken cancellationToken = new CancellationToken())
        {
            await context.Safes.AddAsync(safe);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }

        /// <summary>
        /// Get all safes as paginated.
        /// </summary>
        /// <param name="userId">Filter for for specific owner.</param>
        /// <param name="page">Number of page.</param>
        /// <param name="pageSize">Current page items count.</param>
        /// <param name="keyword">Keyword for search in safe name.</param>
        public async Task<PaginatedList<SafeEntity>> GetAllAsync(int page = 1, int pageSize = 10, string keyword = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var safes = context.Safes.AsQueryable();
            #region Filters
            if (!string.IsNullOrEmpty(keyword))
                safes = safes.Where(b => keyword.Contains(b.Name));
            #endregion
            return await safes.PaginatedListAsync(page, pageSize, cancellationToken);
        }

        /// <summary>
        /// Find a specific safe of a user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SafeEntity> FindByIdAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            var safes = context.Safes.Where(b => b.Id == id);
            return await safes.FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Update a specific safe.
        /// </summary>
        /// <param name="safe">Safe object model to update.</param>
        public async Task<Result> UpdateAsync(SafeEntity safe, CancellationToken cancellationToken = new CancellationToken())
        {
            context.Safes.Update(safe);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }

        /// <summary>
        /// Delete a specific safe.
        /// </summary>
        /// <param name="safe">Safe object model to delete.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result> DeleteAsync(SafeEntity safe, CancellationToken cancellationToken = new CancellationToken())
        {
            context.Safes.Remove(safe);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }
    }
}