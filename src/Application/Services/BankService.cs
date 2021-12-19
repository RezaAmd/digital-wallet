using Application.Interfaces.Context;
using Application.Models;
using Domain.Entities;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BankService
    {
        #region Initialize
        private readonly IDbContext context;
        public BankService(IDbContext _context)
        {
            context = _context;
        }
        #endregion

        /// <summary>
        /// Create a new bank for user.
        /// </summary>
        /// <param name="bank">New bank model object.</param>
        public async Task<Result> CreateAsync(Bank bank, CancellationToken cancellationToken = new CancellationToken())
        {
            await context.Banks.AddAsync(bank);
            if(Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }

        /// <summary>
        /// Find a specific bank in general banks.
        /// </summary>
        /// <param name="id">Bank id to find.</param>
        public async Task<Bank> FindByIdAsync(string id)
        {
            return await context.Banks.FindAsync(id);
        }

        /// <summary>
        /// Find a specific bank of a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Bank> FindByIdAsync(string userId, string id)
        {
            return await context.Banks.Where(b=>b.Id == id && b.OwnerId == userId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Update a specific bank.
        /// </summary>
        /// <param name="bank">Bank object model to update.</param>
        public async Task<Result> UpdateAsync(Bank bank, CancellationToken cancellationToken = new CancellationToken())
        {
            context.Banks.Update(bank);
            if(Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }

        /// <summary>
        /// Delete a specific bank.
        /// </summary>
        /// <param name="bank">Bank object model to delete.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result> DeleteAsync(Bank bank, CancellationToken cancellationToken = new CancellationToken())
        {
            context.Banks.Remove(bank);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }
    }
}
