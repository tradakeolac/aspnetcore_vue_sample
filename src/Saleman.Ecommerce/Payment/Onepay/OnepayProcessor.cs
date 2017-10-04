namespace Saleman.Ecommerce.Payment.Onepay
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Collections.Specialized;
    using WebFramework.Infrastructure.Helpers;
    using Saleman.Ecommerce.Payment.Configurations;
    using Microsoft.Extensions.Configuration;

    public class OnepayProcessor : IPaymentProcessor
    {
        readonly SortedList<string, string> SortedValues = new SortedList<string, string>(new OnepayStringComparable());
        readonly IOnepayEncryptorStrategy EncryptStrategy;
        readonly IHttpUtility EncodedUtility;
        readonly IConfiguration Configuration;

        protected OnePayOptions Options
        {
            get {
                var o = this.Configuration.GetValue<OnePayOptions>("PaymentSection:Onepay");
                return new OnePayOptions();
            }
        }

        public OnepayProcessor(IOnepayEncryptorStrategy encryptStrategy, IHttpUtility httpUtility,
            IConfiguration configuration)
        {
            this.EncryptStrategy = encryptStrategy;
            this.EncodedUtility = httpUtility;
            this.Configuration = configuration;
        }
        
        public PaymentRequestResult Process(PaymentRequest paymentRequest)
        {
            this.AddQueryParams(paymentRequest as OnepayPaymentRequest);

            return this.CreateRedirectRequest();
        }

        public PaymentResponseResult Verify(NameValueCollection callbackParams)
        {
            var payments = this.Configuration.GetSection("PaymentSection:Payments");            
            
            var _responseFields = new SortedList<string, string>(new OnepayStringComparable());
            foreach (string item in callbackParams)
            {
                if (!item.Equals(OnepayParamKeys.SecureHash) && !item.Equals(OnepayParamKeys.SecureHashType))
                {
                    _responseFields.Add(item, callbackParams[item]);
                }
            }

            if (!callbackParams[OnepayParamKeys.TxnResponseCode].Equals("0") && !String.IsNullOrEmpty(callbackParams[OnepayParamKeys.Message]))
            {
                if (!String.IsNullOrEmpty(callbackParams[OnepayParamKeys.SecureHash]))
                {
                    if (!EncryptStrategy.MakeToken(_responseFields, Options.Salt).Equals(callbackParams[OnepayParamKeys.SecureHash]))
                    {
                        PaymentResponseResult.CreateInvalidResult();
                    }
                    return PaymentResponseResult.CreateSuccessResult(ExtractResponse(callbackParams));
                }
                return PaymentResponseResult.CreateSuccessResult(ExtractResponse(callbackParams));
            }

            if (String.IsNullOrEmpty(callbackParams[OnepayParamKeys.SecureHash]))
            {
                return PaymentResponseResult.CreateInvalidResult();//no sercurehash response
            }
            if (!EncryptStrategy.MakeToken(_responseFields, Options.Salt).Equals(callbackParams[OnepayParamKeys.SecureHash]))
            {
                return PaymentResponseResult.CreateInvalidResult();
            }
            return PaymentResponseResult.CreateSuccessResult(ExtractResponse(callbackParams));

        }

        #region Private & sub methods

        private void AddQueryParams(OnepayPaymentRequest model)
        {
            SortedValues.Add(OnepayParamKeys.Title, "onepay paygate");
            SortedValues.Add(OnepayParamKeys.Locale, "vn");
            SortedValues.AddOnePayParam(OnepayParamKeys.Version, Options.Version);
            SortedValues.AddOnePayParam(OnepayParamKeys.Command, Options.Command);
            SortedValues.AddOnePayParam(OnepayParamKeys.Merchant, Options.Merchant);
            SortedValues.AddOnePayParam(OnepayParamKeys.AccessCode, Options.AccessCode);
            SortedValues.AddOnePayParam(OnepayParamKeys.MerchTxnRef, model.MerchRef.ToString());
            SortedValues.AddOnePayParam(OnepayParamKeys.OrderInfo, model.OrderInfo);
            SortedValues.AddOnePayParam(OnepayParamKeys.Amount, (model.Amount * 100).ToString());
            SortedValues.AddOnePayParam(OnepayParamKeys.Currency, model.Currency);
            SortedValues.AddOnePayParam(OnepayParamKeys.ReturnUrl, Options.ReturnUrl);
            SortedValues.Add(OnepayParamKeys.ShipStreet1, "194 Tran Quang Khai");
            SortedValues.Add(OnepayParamKeys.ShipProvice, "Hanoi");
            SortedValues.Add(OnepayParamKeys.ShipCity, "Hanoi");
            SortedValues.Add(OnepayParamKeys.ShipCountry, "Vietnam");
            SortedValues.Add(OnepayParamKeys.CustomerPhone, "043966668");
            SortedValues.Add(OnepayParamKeys.CustomerEmail, "support@onepay.vn");
            SortedValues.Add(OnepayParamKeys.CustomerId, "onepay_paygate");
            SortedValues.AddOnePayParam(OnepayParamKeys.TicketNo, null);
        }

        private PaymentRequestResult CreateRedirectRequest()
        {
            var data = String.Join("&", SortedValues.Keys.Select(key => $"{key}={this.EncodedUtility.UrlEncode(SortedValues[key])}"));
            //Payment Server URL
            var url = Options.ProviderUrl + "?" + data;
            //Hash the request fields
            url += "&" + OnepayParamKeys.SecureHash + "=";
            var token = this.EncryptStrategy.MakeToken(SortedValues, Options.Salt);
            url += token;
            return new PaymentRequestResult
            {
                Url = url,
                Token = token
            };
        }

        private static OnepayPaymentResponse ExtractResponse(NameValueCollection query) => new OnepayPaymentResponse
        {
            Amount = query[OnepayParamKeys.Amount],
            Command = query[OnepayParamKeys.Command],
            Locale = query[OnepayParamKeys.Locale],
            MerchRef = query[OnepayParamKeys.MerchTxnRef],
            Message = query[OnepayParamKeys.Message],
            OrderInfo = query[OnepayParamKeys.OrderInfo],
            TransactionNo = query[OnepayParamKeys.TransactionNo]
        };

        #endregion
    }
}
