using payment_bac.api.Models;

namespace payment_bac.api.DTO
{
    public class PolicyDTO
    {
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyDescription { get; set; }
        public double PolicyTotal { get; set; }
        public double PolicyPaid { get; set; }
        public DateTime PolicyDue { get; set; }

        public PolicyDTO(Policy policy)
        {
            this.PolicyId = policy.ID;
            this.PolicyName = policy.PolicyName;
            this.PolicyDescription = policy.PolicyDescription;
            this.PolicyTotal = policy.PolicyTotal;
            this.PolicyPaid = policy.AmountPaid;
            this.PolicyDue = policy.PolicyDue;
        }
    }
}
