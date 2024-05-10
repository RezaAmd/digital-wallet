using DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalWallet.Application.Dao.Identity
{
    public interface IRoleService
    {
        Task<PaginatedList<RoleEntity>> GetAllAsync(string keyword, int page = 1, int pageSize = 30,
            CancellationToken cancellationToken = default);

        Task<RoleEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Result> CreateAsync(RoleEntity role, CancellationToken cancellationToken = default);

        Task<Result> UpdateAsync(RoleEntity role, CancellationToken cancellationToken = default);

        Task<Result> DeleteAsync(RoleEntity role, CancellationToken cancellationToken = default);
    }
}