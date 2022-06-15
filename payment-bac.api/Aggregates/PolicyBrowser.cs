using payment_bac.api.DataAccess;
using payment_bac.api.Models;

namespace payment_bac.api.Aggregates
{
    public interface IPolicyBrowser
    {
        public Policy GetPolicy(string sessionId);
    }

    public class PolicyBrowser : IPolicyBrowser
    {
        private readonly IRepository _repository;

        public PolicyBrowser(IRepository repository)
        {
            _repository = repository;
        }

        public Policy GetPolicy(string sessionId)
        {
            return _repository.GetPolicy(sessionId);
        }
    }
}
