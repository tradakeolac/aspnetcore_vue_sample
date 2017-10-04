namespace Saleman.Ecommerce.Payment
{
    public class PaymentResponseResult
    {
        public PaymentResult StateResult { get; set; }
        public PaymentResponse Response { get; set; }

        protected PaymentResponseResult()
        {

        }

        public static PaymentResponseResult CreateSuccessResult(PaymentResponse response) =>
            new PaymentResponseResult() { StateResult = PaymentResult.Success, Response = response };

        public static PaymentResponseResult CreateInvalidResult() => new PaymentResponseResult() { StateResult = PaymentResult.Invalid };
    }
}
