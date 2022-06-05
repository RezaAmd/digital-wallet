using Application.Services.WebService.ZarinPal.Model;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Service;
using RestSharp.Service.Models;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.WebService.ZarinPal
{
    public class ZarinpalWebService : IZarinpalWebService
    {
        #region Dependency Injection
        private readonly string merchantId = "c581ad20-7ef9-4553-ae6f-6f175c39af9e";
        //private readonly string callbackUrl = "https://wallet.techonit.org/payment/zarinpalcallback";
        private readonly string callbackUrl = "https://localhost:5001/payment/zarinpalcallback";
        private readonly string baseUrl = "https://api.zarinpal.com/pg/";
        private readonly IRestService restService;

        public ZarinpalWebService(IRestService _restService)
        {
            restService = _restService;
        }
        #endregion

        public async Task<(IRestResponse Response, ResultZarinPal<PaymentRequestResultZarinPal> Result)> PaymentRequestAsync(double amount,
            string description, string mobile = null, string email = null)
        {
            var paymentRequestDto = new PatmentRequestZarinPal(amount * 10, description, callbackUrl, mobile, email);
            var response = await restService
                .RequestAsync(baseUrl + "v4/payment/request.json",
                Method.POST, new RestConfig(body: paymentRequestDto));
            var result = new ResultZarinPal<PaymentRequestResultZarinPal>();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<ResultZarinPal<PaymentRequestResultZarinPal>>(response.Content);
            }
            return (response, result);
        }

        public async Task<(IRestResponse Response, ResultZarinPal<ZarinpalVerifyPaymentResponse> Result)> VerifyPaymentAsync(double amount,
            string authority, CancellationToken cancellationToken = default)
        {
            var verifyPaymentDto = new ZarinpalPaymentVerifyParams(authority, amount * 10);
            var response = await restService.RequestAsync(baseUrl + "v4/payment/verify.json",
                Method.POST, new RestConfig(body: verifyPaymentDto));
            var result = new ResultZarinPal<ZarinpalVerifyPaymentResponse>();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                 result = JsonConvert.DeserializeObject<ResultZarinPal<ZarinpalVerifyPaymentResponse>>(response.Content);
            }
            return (response, result);
        }
    }
}