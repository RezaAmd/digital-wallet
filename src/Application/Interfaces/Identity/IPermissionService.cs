using Application.Models;
using Domain.Entities.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Identity
{
    public interface IPermissionService
    {
        Task<PaginatedList<Permission>> GetAllAsync(string keyword = null, int page = 1, int pageSize = 15,
            bool withRoles = false,
            CancellationToken cancellationToken = default);
        Task<Permission?> FindByIdAsync(params object?[]? id);

        Task<Result> CreateAsync(Permission role, CancellationToken cancellationToken = new());

        Task<Result> UpdateAsync(Permission role, CancellationToken cancellationToken = new());

        Task<Result> DeleteAsync(Permission role, CancellationToken cancellationToken = new());
    }
}