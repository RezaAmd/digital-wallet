using Application.Extentions;
using Application.Interfaces.Context;
using Application.Interfaces.Identity;
using Application.Models;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.Identity
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        #region Constructor
        //private readonly IRoleStore<Role> roleStore;
        //private readonly IEnumerable<IRoleValidator<Role>> roleValidator;
        //private readonly ILookupNormalizer keyNormalizer;
        //private readonly ErrorDescriber error;
        //private readonly ILogger<RoleManager<Role>> logger;
        private readonly IDbContext context;
        public RoleService(IDbContext _context) : base(_context)
        {
            context = _context;
        }

        #endregion

        public async Task<IList<Role>> GetAllAsync(int page = 1, int? pageSize = default)
        {
            var roles = context.Roles.AsQueryable();
            #region Filter
            if (pageSize.HasValue)
            {
                roles = roles.Skip((page - 1) * pageSize.Value).Take(pageSize.Value);
            }
            #endregion
            return await roles.ToListAsync();
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