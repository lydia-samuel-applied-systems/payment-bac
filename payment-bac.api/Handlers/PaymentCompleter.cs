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
            // TODO: possibly this will not be injected as NSB might instantiate it before the injector
            _repository = repository;
        }

        public Task Handle(PaymentFinished message, IMessageHandlerContext context)
        {
            _repository.MarkSessionAsComplete(message.SessionId);

            return Task.CompletedTask;
        }
    }
}
