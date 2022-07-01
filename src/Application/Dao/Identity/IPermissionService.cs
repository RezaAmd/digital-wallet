using  DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace  DigitalWallet.Application.Dao
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
        Task<PaginatedList<Permission>> GetAllAsync(string keyword = null, int page = 1, int pageSize = 15,
            bool withRoles = false,
            CancellationToken cancellationToken = default);
        Task<Permission?> FindByIdAsync(string id, CancellationToken cancellationToken = new());

        Task<Result> CreateAsync(Permission role, CancellationToken cancellationToken = new());

        Task<Result> UpdateAsync(Permission role, CancellationToken cancellationToken = new());

        Task<Result> DeleteAsync(Permission role, CancellationToken cancellationToken = new());
    }
}