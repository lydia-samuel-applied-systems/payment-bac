using NServiceBus;

namespace payment_bac.acl.messages
{
    public class ProcessBankPayment : IMessage
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string SortCode { get; set; }
        public double Amount { get; set; }
    }
}
