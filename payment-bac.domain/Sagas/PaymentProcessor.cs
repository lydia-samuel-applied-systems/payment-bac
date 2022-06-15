using NServiceBus;
using payment_bac.acl.messages;
using payment_bac.domain.messages;

namespace payment_bac.domain.Sagas
{
    public class PaymentProcessor : 
        Saga<PaymentProcessingData>,
        IAmStartedByMessages<PaymentStarted>,
        IHandleTimeouts<PaymentEscalation>
    {
        public async Task Handle(PaymentStarted message, IMessageHandlerContext context)
        {
            Data.PolicyId = message.PolicyId;
            Data.Amount = message.PolicyId;

            // Send message to ACL to talk to AppliedBank
            var bankPayment = new ProcessBankPayment()
            {
                AccountName = message.AccountName,
                AccountNumber = message.AccountNumber,
                SortCode = message.SortCode,
                Amount = message.Amount
            };

            await context.Send(bankPayment);

            // Start timeout for response back from ACL

            await RequestTimeout(context, TimeSpan.FromSeconds(20), new PaymentEscalation());

            // TODO: implement receive code

            // If it doesn't timeout send a message back to the PaymentCompleter in the API

            var finishPayment = new PaymentFinished()
            {
                SessionId = Data.SessionId,
                PolicyId = Data.PolicyId
            };

            await context.Send(finishPayment);
        }

        public Task Timeout(PaymentEscalation state, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<PaymentProcessingData> mapper)
        {
            mapper.MapSaga(saga => saga.SessionId)
                .ToMessage<PaymentStarted>(message => message.SessionId);
        }
    }

    public class PaymentProcessingData : ContainSagaData
    {
        public string SessionId { get; set; }
        public int PolicyId { get; set; }
        public double Amount { get; set; }
    }

    public class PaymentEscalation
    {
    }
}
