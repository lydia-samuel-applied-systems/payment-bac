using payment_bac.api.DataAccess;
using payment_bac.api.Models;
using payment_bac.api.Utilities;

namespace payment_bac.api.Aggregates
{
    public interface ISessionCreator
    {
        public string CreateSession();
    }

    public class SessionCreator : ISessionCreator
    {
        private readonly IRepository _repository;
        private readonly ISessionIdGenerator _sessionIdGenerator;
        private readonly IPolicyGenerator _policyGenerator;

        public SessionCreator(IRepository repository,
            ISessionIdGenerator sessionIdGenerator, 
            IPolicyGenerator policyGenerator)
        {
            _repository = repository;
            _sessionIdGenerator = sessionIdGenerator;
            _policyGenerator = policyGenerator;
        }

        public string CreateSession()
        {
            var sessionId = _sessionIdGenerator.GenerateSessionId();

            var policy = _policyGenerator.MakePolicy();
            
            var session = new Session()
            {
                ID = sessionId,
                IsComplete = false,
                Policy = policy
            };

            _repository.AddSession(session);

            return sessionId;
        }
    }
}
