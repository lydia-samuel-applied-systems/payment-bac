namespace payment_bac.api.Models
{
    public class Policy
    {
        public int ID { get; set; }

        public string PolicyName { get; set; }
        public string PolicyDescription { get; set; }
        public double PolicyTotal { get; set; }
        public double AmountPaid { get; set; }
        public DateTime PolicyDue { get; set; }
    }
}
