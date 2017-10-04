namespace Saleman.Ecommerce.Payment.Paypal
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Text;

    public class PaypalProcessor : IPaymentProcessor
    {
        public PaymentRequestResult Process(PaymentRequest paymentRequest)
        {
            throw new NotImplementedException();
        }

        public PaymentResponseResult Verify(NameValueCollection callbackParams)
        {
            throw new NotImplementedException();
        }
    }
}
