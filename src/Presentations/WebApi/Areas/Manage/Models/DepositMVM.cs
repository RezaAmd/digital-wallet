using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Enums;
using Mapster;

namespace DigitalWallet.WebApi.Areas.Manage.Models;
#nullable disable
public class DepositMVM
{
    public string TraceId { get; set; }
    public double amount { get; set; }
    public string destinationId { get; set; }
    public DepositState state { get; set; }
    public string dateTime { get; set; }

    public static TypeAdapterConfig MapConfig()
    {
        return TypeAdapterConfig<DepositEntity, DepositMVM>.NewConfig()
            .Map(dest => dest.amount, src => src.Amount.Value).Config;
    }
}