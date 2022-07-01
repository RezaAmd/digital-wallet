using DigitalWallet.Application.Dao;
using DigitalWallet.Application.Models;
using DigitalWallet.Application.Repositories;
using DigitalWallet.Application.Services.Identity;
using DigitalWallet.Application.Services.WebService.ZarinPal;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalWallet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Service Life Time
        services.AddScoped<ErrorDescriber>()
            .AddHttpContextAccessor()
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddTransient<IMapper, Mapper>();

        #region Web Service
        services
            .AddTransient<IZarinpalWebService, ZarinpalWebService>()
            ;
        #endregion

        TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);

        return services;
    }

    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>()
            .AddScoped<IRoleService, RoleDao>()
            .AddScoped<IPermissionService, PermissionDao>()
            .AddScoped<IWalletRepository, WalletRepository>()
            .AddScoped<ITransferRepository, TransferRepository>()
            .AddScoped<IDepositRepository, DepositRepository>()
            ;
        return services;
    }
}