using NServiceBus;
using payment_bac.acl.messages;

namespace payment_bac.acl.Handlers
{
    public class AppliedBankTeller : IHandleMessages<ProcessBankPayment>
    {
        public Task Handle(ProcessBankPayment message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }
    }
}
