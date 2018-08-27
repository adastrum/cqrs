using System.ComponentModel.DataAnnotations;

namespace cqrs.Web.MVC.Models
{
    public class CreateAuctionViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        [Required]
        public decimal InitialAmount { get; set; }
    }
}
