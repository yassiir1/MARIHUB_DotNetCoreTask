using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using MIRAHUB.Models;

namespace MIRAHUB.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly MIRAHUBDb _context;
        public OrderServices(MIRAHUBDb context)
        {
            _context = context;
        }

        public string AddOrder(List<AddOrder> Order, string UserName)
        {
            var Status = "";
            try
            {
                var OrderHeader = new Orders
                {
                    OrderGUID = Guid.NewGuid(),
                    OrderDate = DateTime.Now,
                    user = UserName,
                    account = UserName,
                    TotalPrice = Order.Sum(u => (u.price * u.quantity))
                };
                _context.Orders.Add(OrderHeader);
                _context.SaveChanges();
                foreach (var item in Order)
                {
                    var Orders = new OrderDetails
                    {
                        OrderGUID = OrderHeader.OrderGUID,
                        ProductName = item.productname,
                        Price = item.price,
                        Quantity = item.quantity,
                        TotalPrice = item.price * item.quantity,

                    };
                    _context.OrderDetails.Add(Orders);
                    _context.SaveChanges();
                }
                Status = "Success";
            }
            catch(Exception ex)
            {
                Status = "Failed";
            }
            return Status;
        }

        public string DeleteOrder(int id)
        {
            var Status = "";
            var Order = _context.Orders.Find(id);
            if (Order == null)
                return "Not Found";
            try
            {
                _context.Orders.Remove(Order);
                _context.SaveChanges();
                Status = "Success";
            }
            catch (Exception ex)
            {
                Status = "Failed";
            }
            return Status;
        }

        public Orders GetOrderByID(int id)
        {
            var data = _context.Orders.Find(id);
            return data;
        }

        public List<Orders> GetOrders()
        {
            var data = _context.Orders.ToList();
            data.Select(u => u.OrderDetails = _context.OrderDetails.Where(c => c.OrderGUID == u.OrderGUID).ToList()).ToList();
            return data;
        }

        public string UpdateOrder(Orders Order)
        {
            _context.Entry(Order).State = EntityState.Modified;
            var Status = "";
            try
            {
                _context.SaveChangesAsync();
                Status = "Success";

            }
            catch (Exception ex)
            {
                Status = "Failes";
            }
            return Status;
        }
    }
}
