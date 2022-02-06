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
            // Identity Service Life Time
            services.AddScoped<ErrorDescriber>()
                .AddHttpContextAccessor()
                .AddScoped<IUserService, UserService>()
                .AddTransient<ISignInService, SignInService>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<IPermissionService, PermissionService>()
                .AddScoped<IWalletService, WalletService>()
                .AddScoped<ITransferService, TransferService>()
                .AddScoped<IDepositService, DepositService>()
                .AddTransient<IMapper, Mapper>();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.IgnoreNullValues = true;
            //})

            return services;
        }

    }
}