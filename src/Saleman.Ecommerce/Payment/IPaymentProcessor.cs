namespace Saleman.Ecommerce.Payment
{
    using System.Collections.Specialized;

    public interface IPaymentProcessor
    {
        PaymentRequestResult Process(PaymentRequest paymentRequest);
        PaymentResponseResult Verify(NameValueCollection callbackParams);
    }
}
