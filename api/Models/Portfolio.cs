using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Portfolios")]
    public class Portfolios
    {
        public string AppUserId { get; set; }
        public int StockId { get; set; }
        public AppUser AppUser { get; set; }
        public Stock Stock { get; set; }
    }
}