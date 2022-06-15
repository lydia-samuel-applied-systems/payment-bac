namespace payment_bac.api.Models
{
    public class Session
    {
        public string ID { get; set; }

        public bool IsComplete { get; set; }
        public Policy Policy { get; set; }
    }
}
