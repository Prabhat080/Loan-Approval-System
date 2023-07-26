namespace Bank.Authentication
{
    public class LoanUser
    {
        public int id { get; set; }
        public string email { get; set; }
        public double loan { get; set; }
        public int status { get; set; }
        public DateTime processed_date { get; set; }
    }
}
