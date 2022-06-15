using payment_bac.api.Aggregates;
using payment_bac.api.DTO;

namespace payment_bac.api.Handlers
{
    public interface ISessionHandler
    {
        public string StartSession();
        public PolicyDTO GetSessionPolicy(string sessionId);
    }

    public class SessionHandler : ISessionHandler
    {
        private readonly ISessionCreator _sessionCreator;
        private readonly IPolicyBrowser _policyBrowser;

        public SessionHandler(ISessionCreator sessionCreator, IPolicyBrowser policyBrowser)
        {
            _sessionCreator = sessionCreator;
            _policyBrowser = policyBrowser;
        }

        public string StartSession()
        {
            return _sessionCreator.CreateSession();
        }

        public PolicyDTO GetSessionPolicy(string sessionId)
        {
            var policy = _policyBrowser.GetPolicy(sessionId);

            var dto = new PolicyDTO(policy);

            return dto;
        }
    }
}
