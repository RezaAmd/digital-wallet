using Application.Models;
using Domain.Entities.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Identity
{
    public interface IRoleService
    {
        Task<IList<Role>> GetAllAsync(int page = 1, int? pageSize = default);

        Task<Role?> FindByIdAsync(params object?[]? id);

        Task<Result> CreateAsync(Role role, CancellationToken cancellationToken = new());
        
        Task<Result> UpdateAsync(Role role, CancellationToken cancellationToken = new());

        Task<Result> DeleteAsync(Role role, CancellationToken cancellationToken = new());
    }
}