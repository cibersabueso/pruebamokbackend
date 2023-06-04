using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderId { get; set; }
        [Key]
        public int ProductId { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? Quantity { get; set; }
        public float? Discount { get; set; }

        public Orders Order { get; set; }
    }
}
