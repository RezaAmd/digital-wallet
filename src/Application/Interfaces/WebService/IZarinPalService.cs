using System.Threading.Tasks;

namespace Application.Interfaces.WebService
{
    public interface IZarinPalService
    {
        Task<string> PaymentRequestAsync(double amount, string description, string mobile);
    }
}