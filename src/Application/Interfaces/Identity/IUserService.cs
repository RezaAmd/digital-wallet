using Application.Models;
using Domain.Entities.Identity;
using Domain.Enums;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Identity
{
    public interface IUserService
    {
        Task<PaginatedList<TDestination>> GetAllAsync<TDestination>(TypeAdapterConfig config = null, int page = 1, int pageSize = 20,
            bool withRoles = false, string keyword = default, bool tracking = false, CancellationToken cancellationToken = new CancellationToken());
        Task<User> FindByIdentityAsync(string identity, bool asNoTracking = false, bool withRoles = false,
            TypeAdapterConfig config = null);
        (Result Status, string Token) GenerateJwtToken(User user, DateTime? expire = default);
        string GenerateOtp(string phoneNumber, HttpContext httpContext);
        (Result Result, string PhoneNumber) VerifyOtp(string code, HttpContext httpContext);
    }
}