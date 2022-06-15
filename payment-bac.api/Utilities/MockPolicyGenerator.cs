using payment_bac.api.Models;

namespace payment_bac.api.Utilities
{
    public interface IPolicyGenerator
    {
        public Policy MakePolicy();
    }

    public class MockPolicyGenerator : IPolicyGenerator
    {
        public Policy MakePolicy()
        {
            return new Policy()
            {
                PolicyName = "Lorem ipsum",
                PolicyDescription = "dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                PolicyTotal = 420.0,
                AmountPaid = 69.0,
                PolicyDue = DateTime.Now + TimeSpan.FromDays(1)
            };
        }
    }
}
