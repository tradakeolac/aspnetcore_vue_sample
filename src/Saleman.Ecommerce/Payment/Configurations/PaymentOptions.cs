namespace Saleman.Ecommerce.Payment.Configurations
{
    public class PaymentOptions
    {
        public string ProviderUrl { get; set; }

        public string ReturnUrl { get; set; }

        public string Salt { get; set; }
    }
}
