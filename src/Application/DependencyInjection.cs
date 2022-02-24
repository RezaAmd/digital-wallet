using Application.Dao;
using Application.Interfaces;
using Application.Interfaces.Identity;
using Application.Models;
using Application.Services;
using Application.Services.Identity;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLogicServices(this IServiceCollection services)
        {
            // Service Life Time
            services.AddScoped<ErrorDescriber>()
                .AddHttpContextAccessor()
                .AddTransient<ISignInService, SignInService>()
                .AddTransient<IMapper, Mapper>();

            #region DAO Services
            services.AddScoped<IUserService, UserService>()
                .AddScoped<IRoleService, RoleDao>()
                .AddScoped<IPermissionService, PermissionDao>()
                .AddScoped<IWalletDao, WalletDao>()
                .AddScoped<ITransferDao, TransferDao>()
                .AddScoped<IDepositDao, DepositDao>();

            #endregion

            return services;
        }

    }
}