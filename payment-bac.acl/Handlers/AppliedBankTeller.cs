using NServiceBus;
using payment_bac.acl.messages;

namespace payment_bac.acl.Handlers
{
    public class AppliedBankTeller : IHandleMessages<ProcessBankPayment>
    {
        public async Task Handle(ProcessBankPayment message, IMessageHandlerContext context)
        {
            var response = new PaymentProcessed();

            // Do some stuff
            Thread.Sleep(5000);

            await context.Reply(response);
        }
    }
}
