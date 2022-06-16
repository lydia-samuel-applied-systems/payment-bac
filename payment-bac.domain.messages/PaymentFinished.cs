using NServiceBus;

namespace payment_bac.domain.messages
{
    public class PaymentFinished : IMessage
    {
        public string SessionId { get; set; }
        public double Amount { get; set; }
        public bool Success { get; set; }
        public int PolicyId { get; set; }
    }
}
