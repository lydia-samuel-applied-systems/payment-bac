using Microsoft.AspNetCore.Mvc;

namespace payment_bac.api.Controllers
{
    [Route("/bac-api/")]
    public class ApiController : Controller
    {
        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public void CreateSession()
        {
            // Todo: Learn how to generate a session and store session state in the DB
            // return UI url
        }

        [HttpGet]
        public void GetSessionData()
        {
            // Todo: Learn how to lookup session state and grab the policy from the DB
            // UI should hit this and it should return the randomly generated policy
        }

        [HttpPost]
        public void SubmitPaymentDetails()
        {
            // Todo: Learn how to raise an event that gets picked up by the Domain which starts a saga
            // Do not persist the details here, instead raise an event which the domain should start a saga from
        }
    }
}