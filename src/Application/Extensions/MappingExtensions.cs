using DigitalWallet.Application.Models;
using Mapster;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalWallet.Application.Extensions
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TOrigin>> PaginatedListAsync<TOrigin>(this IQueryable<TOrigin> queryable, int pageNumber,
            int pageSize, CancellationToken cancellationToken = default)
            => PaginatedList<TOrigin>.CreateAsync(queryable, pageNumber, pageSize, cancellationToken);

        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TOrigin, TDestination>(this IQueryable<TOrigin> queryable,
            int pageNumber, int pageSize, CancellationToken cancellationToken = default, TypeAdapterConfig config = default)
            => PaginatedList<TDestination>.CreateAsync<TOrigin, TDestination>(queryable, pageNumber, pageSize,
                cancellationToken, config);
    }
}