using Application.Extentions;
using Application.Interfaces.Context;
using Application.Models;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Dao
{
    public class RoleDao : BaseDao<Role, string>, IRoleService
    {
        #region Constructor
        //private readonly IRoleStore<Role> roleStore;
        //private readonly IEnumerable<IRoleValidator<Role>> roleValidator;
        //private readonly ILookupNormalizer keyNormalizer;
        //private readonly ErrorDescriber error;
        //private readonly ILogger<RoleManager<Role>> logger;
        private readonly IDbContext context;
        public RoleDao(IDbContext _context) : base(_context)
        {
            context = _context;
        }

        #endregion

        public async Task<PaginatedList<Role>> GetAllAsync(string keyword, int page = 1, int pageSize = 30,
            CancellationToken cancellationToken = new())
        {
            var roles = context.Roles.AsQueryable();
            #region Filter
            // Search with name, title, description
            if (!string.IsNullOrEmpty(keyword))
                roles = roles.Where(x => x.Name.Contains(keyword)
                || x.Title.Contains(keyword)
                || x.Description.Contains(keyword));
            #endregion
            return await roles.PaginatedListAsync(page, pageSize, cancellationToken);
        }

        public async Task<Result> CreateRangeAsync(List<Role> roles)
        {
            try
            {
                context.Roles.AddRange(roles);
                if (Convert.ToBoolean(await context.SaveChangesAsync(new CancellationToken())))
                    return Result.Success;
            }
            catch (DbUpdateException ex)
            {
                return Result.Failed(new() { ex.HandleDbExtentionFilter() });
            }
            return Result.Failed();
        }
    }
}