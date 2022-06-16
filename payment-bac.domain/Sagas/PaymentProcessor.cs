using NServiceBus;
using payment_bac.acl.messages;
using payment_bac.domain.messages;

namespace payment_bac.domain.Sagas
{
    public class PaymentProcessor : 
        Saga<PaymentProcessingData>,
        IAmStartedByMessages<PaymentStarted>,
        IHandleMessages<PaymentProcessed>,
        IHandleTimeouts<PaymentEscalation>
    {
        public async Task Handle(PaymentStarted message, IMessageHandlerContext context)
        {
            Data.PolicyId = message.PolicyId;
            Data.Amount = message.Amount;
            
            var bankPayment = new ProcessBankPayment()
            {
                AccountName = message.AccountName,
                AccountNumber = message.AccountNumber,
                SortCode = message.SortCode,
                Amount = message.Amount
            };

            await context.Send(bankPayment);

            await RequestTimeout(context, TimeSpan.FromMinutes(5), new PaymentEscalation());
        }

        public async Task Handle(PaymentProcessed message, IMessageHandlerContext context)
        {
            if (message.Success)
            {
                var finishPayment = new PaymentFinished()
                {
                    SessionId = Data.SessionId,
                    Amount = Data.Amount,
                    Success = true,
                    PolicyId = Data.PolicyId
                };

                await context.Send(finishPayment);
            }
            else
            {
                var finishPayment = new PaymentFinished()
                {
                    SessionId = Data.SessionId,
                    Amount = 0.0,
                    Success = false,
                    PolicyId = Data.PolicyId
                };

                await context.Send(finishPayment);
            }

            MarkAsComplete();
        }

        public async Task Timeout(PaymentEscalation state, IMessageHandlerContext context)
        {
            var finishPayment = new PaymentFinished()
            {
                SessionId = Data.SessionId,
                Amount = 0.0,
                Success = false,
                PolicyId = Data.PolicyId
            };

            await context.Send(finishPayment);

            MarkAsComplete();
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
