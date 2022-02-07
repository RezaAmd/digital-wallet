using Application.Models;
using Domain.Entities.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Identity
{
    public interface IRoleService
    {
        Task<PaginatedList<Role>> GetAllAsync(string keyword, int page = 1, int pageSize = 30,
            CancellationToken cancellationToken = new());

        Task<Role?> FindByIdAsync(params object?[]? id);

        Task<Result> CreateAsync(Role role, CancellationToken cancellationToken = new());

        Task<Result> UpdateAsync(Role role, CancellationToken cancellationToken = new());

        Task<Result> DeleteAsync(Role role, CancellationToken cancellationToken = new());
    }
}