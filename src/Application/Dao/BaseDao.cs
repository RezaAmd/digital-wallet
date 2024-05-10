using  DigitalWallet.Application.Interfaces.Context;
using  DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace  DigitalWallet.Application.Dao
{
    public class BaseDao<TEntity, TKey> where TEntity : BaseEntity
    {
        #region DI
        private readonly IDbContext context;
        private readonly DbSet<TEntity> entities;
        public BaseDao(IDbContext _context)
        {
            context = _context;
            entities = context.Set<TEntity>();
        }
        #endregion

        public async Task<TEntity?> FindByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await entities.FindAsync(id, cancellationToken);
        }

        public async Task<Result> CreateAsync(TEntity entry, CancellationToken cancellationToken = new CancellationToken())
        {
            await entities.AddAsync(entry);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }

        public async Task<Result> UpdateAsync(TEntity entry, CancellationToken cancellationToken = new CancellationToken())
        {
            entities.Update(entry);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }

        public async Task<Result> DeleteAsync(TEntity role, CancellationToken cancellationToken = new CancellationToken())
        {
            entities.Remove(role);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }
    }
}