using Microsoft.AspNetCore.Mvc;
using payment_bac.api.DTO;
using payment_bac.api.Handlers;

namespace payment_bac.api.Controllers
{
    [ApiController]
    public class SessionController : Controller
    {
        private readonly ISessionHandler _sessionHandler;
        private readonly IPaymentRaiser _paymentRaiser;

        public SessionController(
            ISessionHandler sessionHandler,
            IPaymentRaiser paymentRaiser)
        {
            _sessionHandler = sessionHandler;
            _paymentRaiser = paymentRaiser;
        }
        
        [HttpGet]
        [Route("/api/[controller]/StartSession")]
        public IActionResult StartSession()
        {
            var sessionId = _sessionHandler.StartSession();

            var result = new SessionDTO()
            {
                SessionId = sessionId,
                UiUrl = "www.example.com"
            };

            return Json(result);
        }

        [HttpPost]
        [Route("/api/[controller]/LookupSessionData")]
        public IActionResult LookupSessionData(LookupDTO lookup)
        {
            var policyInfo = _sessionHandler.GetSessionPolicy(lookup.SessionId); // TODO: Handle case where sessionId doesn't exist in DB

            return Json(policyInfo);
        }

        [HttpPost]
        [Route("/api/[controller]/SubmitPaymentDetails")]
        public async Task<IActionResult> SubmitPaymentDetails(PaymentDTO paymentDetails)
        {
            await _paymentRaiser.RaisePayment(paymentDetails);

            var result = new StatusDTO()
            {
                Status = "Success",
                StatusMessage = "Payment was successfully received, please check back later."
            };

            return Json(result);
        }
    }
}