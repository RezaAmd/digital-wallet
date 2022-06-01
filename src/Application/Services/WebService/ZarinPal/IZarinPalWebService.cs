using Application.Services.WebService.ZarinPal.Model;
using RestSharp;
using System.Threading.Tasks;

namespace Application.Services.WebService.ZarinPal
{
    public interface IZarinPalWebService
    {
        Task<string> PaymentRequestAsync(double amount, string description, string mobile);
        Task<(IRestResponse, ResultZarinPal<PaymentRequestResultZarinPal>)> PaymentRequestAsync(double amount, string description, string mobile);
    }
}