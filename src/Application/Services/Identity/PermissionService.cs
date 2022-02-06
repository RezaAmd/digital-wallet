using Application.Interfaces.Context;
using Application.Interfaces.Identity;
using Domain.Entities.Identity;

namespace Application.Services.Identity
{
    public class PermissionService : BaseService<Permission>, IPermissionService
    {
        private readonly IDbContext context;

        public PermissionService(IDbContext _context) : base(_context)
        {
            context = _context;
        }
    }
}