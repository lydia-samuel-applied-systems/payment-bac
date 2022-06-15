namespace payment_bac.api.DTO
{
    public class PaymentDTO
    {
        public int PolicyId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string SortCode { get; set; }
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
