using Application.Services.WebService.ZarinPal.Model;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.WebService.ZarinPal
{
    public interface IZarinpalWebService
    {
        Task<(IRestResponse Response, ResultZarinPal<PaymentRequestResultZarinPal> Result)> PaymentRequestAsync(double amount,
            string description, string mobile = null, string email = null);

        Task<(IRestResponse Response, ResultZarinPal<ZarinpalVerifyPaymentResponse> Result)> VerifyPaymentAsync(double amount,
            string authority, CancellationToken cancellationToken = default);
    }
}