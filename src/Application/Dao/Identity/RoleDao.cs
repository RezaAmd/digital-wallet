using DigitalWallet.Application.Extensions;
using DigitalWallet.Application.Interfaces.Context;
using DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Application.Dao.Identity
{
    public class RoleDao : BaseDao<RoleEntity, string>, IRoleService
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

        public async Task<PaginatedList<RoleEntity>> GetAllAsync(string keyword, int page = 1, int pageSize = 30,
            CancellationToken cancellationToken = default)
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

        public async Task<Result> CreateRangeAsync(List<RoleEntity> roles)
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

        public async Task<RoleEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await context.Roles.FindAsync(id, cancellationToken);
    }
}