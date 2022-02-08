using Application.Interfaces.WebService;
using Infrastructure.Common.Services.WebService.ZarinPal.Model;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Service;
using RestSharp.Service.Models;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services.WebService.ZarinPal
{
    public class ZarinPalService : IZarinPalService
    {
        #region Dependency Injection
        private readonly string callbackUrl = "https://wallet.techonit.org/payment/zarinpalcallback";
        private readonly IRestService restService;

        public ZarinPalService(IRestService _restService)
        {
            restService = _restService;
        }
        #endregion

        public async Task<(IRestResponse, ResultZarinPal<PaymentRequestResultZarinPal>)> PaymentRequestAsync(double amount, string description, string mobile)
        {
            var paymentRequestDto = new PatmentRequestZarinPal(amount, description, callbackUrl, mobile);
            var response = await restService
                .RequestAsync("https://api.zarinpal.com/pg/v4/payment/request.json",
                Method.POST, new RestConfig(body: paymentRequestDto));
            return (response, JsonConvert.DeserializeObject<ResultZarinPal<PaymentRequestResultZarinPal>>(response.Content));
        }

        Task<string> IZarinPalService.PaymentRequestAsync(double amount, string description, string mobile)
        {
            throw new System.NotImplementedException();
        }
    }
}
