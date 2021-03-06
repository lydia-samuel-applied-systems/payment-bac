using NServiceBus;
using payment_bac.api.DTO;
using payment_bac.domain.messages;

namespace payment_bac.api.Handlers
{
    public interface IPaymentRaiser
    {
        public Task RaisePayment(PaymentDTO paymentDetails);
    }

    public class PaymentRaiser : IPaymentRaiser
    {
        private readonly IMessageSession _messageSession;

        public PaymentRaiser(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        public async Task RaisePayment(PaymentDTO paymentDetails)
        {
            var paymentStartedEvent = new PaymentStarted()
            {
                SessionId = paymentDetails.SessionId,
                PolicyId = paymentDetails.PolicyId,
                AccountName = paymentDetails.AccountName,
                AccountNumber = paymentDetails.AccountNumber,
                SortCode = paymentDetails.SortCode,
                Amount = paymentDetails.Amount
            };

            await _messageSession.Publish(paymentStartedEvent);
        }
    }
}
