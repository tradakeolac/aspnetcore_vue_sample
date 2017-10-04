namespace Saleman.Ecommerce.Initialization
{
    using Saleman.Ecommerce.Payment;
    using Saleman.Ecommerce.Payment.Onepay;
    using Saleman.Ecommerce.Payment.Paypal;
    using WebFramework.Infrastructure.Attributes;
    using WebFramework.Infrastructure.Initialization;

    [InitializableModule]
    public class InitializationModule : IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            context.Services.AddSingleton<IPaymentProcessor, OnepayProcessor>(PaymentKeys.OnepayProvider);
            context.Services.AddSingleton<IPaymentProcessor, PaypalProcessor>(PaymentKeys.PaypalProvider);
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}