using DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalWallet.Application.Dao.Identity
{
    public interface IPermissionService
    {
        /// <summary>
        /// Get all permissions
        /// </summary>
        /// <param name="keyword">Keyword for search</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="withRoles"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PaginatedList<PermissionEntity>> GetAllAsync(string keyword = null, int page = 1, int pageSize = 15,
            bool withRoles = false,
            CancellationToken cancellationToken = default);
        Task<PermissionEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Result> CreateAsync(PermissionEntity role, CancellationToken cancellationToken = default);

        Task<Result> UpdateAsync(PermissionEntity role, CancellationToken cancellationToken = default);

        Task<Result> DeleteAsync(PermissionEntity role, CancellationToken cancellationToken = default);
    }
}