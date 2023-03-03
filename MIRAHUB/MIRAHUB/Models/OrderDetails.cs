using System.ComponentModel.DataAnnotations;

namespace MIRAHUB.Models
{
    public class OrderDetails
    {
        [Key]
        public int RecordID { get; set; }
        public Guid OrderGUID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public class AddOrder
    {
        public string productname { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
    }
}
