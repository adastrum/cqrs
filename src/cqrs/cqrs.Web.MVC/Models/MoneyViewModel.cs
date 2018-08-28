namespace cqrs.Web.MVC.Models
{
    public class MoneyViewModel
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }

        public override string ToString()
        {
            return $"{Amount} {CurrencyCode}";
        }
    }
}