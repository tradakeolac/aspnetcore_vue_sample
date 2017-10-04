namespace Saleman.Ecommerce.Payment.Configurations
{
    public class OnePayOptions : PaymentOptions
    {
        public string Version { get; set; }

        public string Command { get; set; }

        public string AccessCode { get; set; }

        public string Merchant { get; set; }
    }
}
