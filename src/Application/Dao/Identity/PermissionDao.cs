using DigitalWallet.Application.Extensions;
using DigitalWallet.Application.Interfaces.Context;
using DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Application.Dao.Identity
{
    public class PermissionDao : BaseDao<PermissionEntity, string>, IPermissionService
    {
        #region DI

        private readonly IDbContext context;

        public PermissionDao(IDbContext _context) : base(_context)
        {
            context = _context;
        }

        #endregion

        public async Task<PermissionEntity?> FindByIdAsync(Guid id,
            CancellationToken cancellationToken = default)
            => await context.Permissions.FindAsync(id, cancellationToken);

        public Task<PaginatedList<PermissionEntity>> GetAllAsync(string keyword = null, int page = 1, int pageSize = 15,
            bool withRoles = false,
            CancellationToken cancellationToken = default)
        {
            var permissions = context.Permissions.AsQueryable();
            #region Filter
            // Search with name, title, description.
            if (!string.IsNullOrEmpty(keyword))
                permissions = permissions.Where(p => p.Name.Contains(keyword)
                || p.Title.Contains(keyword)
                || p.Description.Contains(keyword));

            if (withRoles)
                permissions = permissions.Include(p => p.PermissionRoles).ThenInclude(r => r.Role);
            #endregion
            return permissions.PaginatedListAsync(page, pageSize, cancellationToken);
        }
    }
}