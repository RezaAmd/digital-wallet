using  DigitalWallet.Application.Models;
using System.Threading.Tasks;

namespace DigitalWallet.Infrastructure.Common.Interfaces
{
    public interface ISmsService
    {
        Task<Result> SendAsync(string reciever, string text);
        Task<Result> SendOtpAsync(string reciever, string token);
    }
}