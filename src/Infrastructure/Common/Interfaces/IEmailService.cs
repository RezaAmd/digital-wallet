using  DigitalWallet.Application.Models;
using DigitalWallet.Domain.Enums;
using System.Threading.Tasks;

namespace DigitalWallet.Infrastructure.Common.Interfaces
{
    public interface IEmailService
    {
        Task<Result> SendAsync(string reciever, string subject, string body, FromEmail from = FromEmail.Noreplay, string recieverName = default);
    }
}