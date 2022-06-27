using Application.Extentions;
using Application.Interfaces.Context;
using Application.Models;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Dao
{
    public class PermissionDao : BaseDao<Permission, string>, IPermissionService
    {
        #region DI
        private readonly IDbContext context;

        public PermissionDao(IDbContext _context) : base(_context)
        {
            context = _context;
        }
        #endregion

        public Task<PaginatedList<Permission>> GetAllAsync(string keyword = null, int page = 1, int pageSize = 15,
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