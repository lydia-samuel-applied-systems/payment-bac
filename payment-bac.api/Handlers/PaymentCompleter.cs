using NServiceBus;
using payment_bac.api.DataAccess;
using payment_bac.api.DTO;
using payment_bac.domain.messages;

namespace payment_bac.api.Handlers
{
    public class PaymentCompleter : IHandleMessages<PaymentFinished>
    {
        private readonly IRepository _repository;

        public PaymentCompleter(IRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(PaymentFinished message, IMessageHandlerContext context)
        {
            _repository.PayTowardsPolicy(message.SessionId, message.Amount);
            _repository.MarkSessionAsComplete(message.SessionId);

            return Task.CompletedTask;
        }
    }
}
