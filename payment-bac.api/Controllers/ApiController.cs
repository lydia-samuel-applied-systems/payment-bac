using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using payment_bac.api.DataAccess;
using payment_bac.api.DTO;
using payment_bac.api.Handlers;
using payment_bac.api.Utilities;

namespace payment_bac.api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api")]
    public class ApiController : Controller
    {
        private readonly ISessionHandler _sessionHandler;
        private readonly IPaymentRaiser _paymentRaiser;

        public ApiController(
            ISessionHandler sessionHandler,
            IPaymentRaiser paymentRaiser)
        {
            _sessionHandler = sessionHandler;
            _paymentRaiser = paymentRaiser;
        }

        [Microsoft.AspNetCore.Mvc.Route("/StartSession")]
        [HttpGet]
        public IActionResult StartSession()
        {
            var sessionId = _sessionHandler.StartSession();
  
            // return Json structure with the URL and the session ID

            throw new NotImplementedException();
        }

        [Microsoft.AspNetCore.Mvc.Route("/LookupSessionData")]
        [HttpPost]
        public IActionResult LookupSessionData(string sessionId)
        {
            var policyInfo = _sessionHandler.GetSessionPolicy(sessionId);

            // return Json structure with the policy data for the session

            throw new NotImplementedException();
        }

        [Microsoft.AspNetCore.Mvc.Route("/SubmitPaymentDetails")]
        [HttpPost]
        public async Task SubmitPaymentDetails(string sessionId, PaymentDTO paymentDetails)
        {
            await _paymentRaiser.RaisePayment(sessionId, paymentDetails);

            throw new NotImplementedException();
        }
    }
}