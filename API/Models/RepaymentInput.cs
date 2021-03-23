namespace API.Models
{
    public class RepaymentInput
    {
        public float InterestRate { get; set; }
        public double LoanAmount { get; set; }
        public int InstallmentsYears { get; set; }
    }
}