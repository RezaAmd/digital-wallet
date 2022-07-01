using Domain.Entities;
using Domain.Enums;
using Mapster;

namespace WebApi.Areas.Manage.Models;
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
        return TypeAdapterConfig<Deposit, DepositMVM>.NewConfig()
            .Map(dest => dest.amount, src => src.Amount.Value).Config;
    }
}