using System.ComponentModel.DataAnnotations;

namespace MIRAHUB.Models
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public int Account_RemainingBudget { get; set; }
        public DateTime OrderDate { get; set; }
        public string account { get; set; }
        public string user { get; set; }
        public Guid OrderGUID { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }

}
