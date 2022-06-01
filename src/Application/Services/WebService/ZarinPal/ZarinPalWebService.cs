using Application.Interfaces.WebService;
using Application.Services.WebService.ZarinPal.Model;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Service;
using RestSharp.Service.Models;
using System.Threading.Tasks;

namespace Application.Services.WebService.ZarinPal
{
    public class ZarinPalWebService : IZarinPalWebService
    {
        #region Dependency Injection
        private readonly string callbackUrl = "https://wallet.techonit.org/payment/zarinpalcallback";
        private readonly string baseUrl = "https://api.zarinpal.com/pg/";
        private readonly IRestService restService;

        public ZarinPalWebService(IRestService _restService)
        {
            restService = _restService;
        }
        #endregion

        public async Task<(IRestResponse, ResultZarinPal<PaymentRequestResultZarinPal>)> PaymentRequestAsync(double amount, string description, string mobile)
        {
            var paymentRequestDto = new PatmentRequestZarinPal(amount, description, callbackUrl, mobile);
            var response = await restService
                .RequestAsync(baseUrl + "v4/payment/request.json",
                Method.POST, new RestConfig(body: paymentRequestDto));
            return (response, JsonConvert.DeserializeObject<ResultZarinPal<PaymentRequestResultZarinPal>>(response.Content));
        }

        public async Task<string> PaymentRequestAsync(double amount, string description, string mobile)
        {
            var paymentRequestDto = new PatmentRequestZarinPal(amount, description, callbackUrl, mobile);
            var response = await restService
                .RequestAsync(baseUrl + "v4/payment/request.json",
                Method.POST, new RestConfig(body: paymentRequestDto));

        }
    }
}