using  DigitalWallet.Application.Services.WebService.ZarinPal.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Service;
using RestSharp.Service.Models;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace  DigitalWallet.Application.Services.WebService.ZarinPal
{
    public class ZarinpalWebService : IZarinpalWebService
    {
        #region Dependency Injection
        //private readonly string callbackUrl = "https://wallet.techonit.org/payment/zarinpalcallback";
        private readonly string callbackUrl = "https://localhost:5001/payment/zarinpalcallback";
        private readonly string baseUrl = "https://api.zarinpal.com/pg/";
        private readonly IRestService restService;
        private readonly ILogger<ZarinpalWebService> _logger;

        public ZarinpalWebService(IRestService _restService,
            ILogger<ZarinpalWebService> logger)
        {
            restService = _restService;
            _logger = logger;
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
            var result = new ResultZarinPal<ZarinpalVerifyPaymentResponse>();
            try
            {
                var verifyPaymentDto = new ZarinpalPaymentVerifyParams(authority, amount * 10);
                var response = await restService.RequestAsync(baseUrl + "v4/payment/verify.json",
                    Method.POST, new RestConfig(body: verifyPaymentDto));
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<ResultZarinPal<ZarinpalVerifyPaymentResponse>>(response.Content);
                }
                return (response, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return (null, result);
        }
    }
}