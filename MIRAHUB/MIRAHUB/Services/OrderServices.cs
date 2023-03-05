using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            var BudgetPerMonth = 10;
            var Month = DateTime.Now.Month;
            var Remaining = 0;
            var Year = DateTime.Now.Year;
            var AccountBudget = "";
            try
            {
                 AccountBudget = _context.Orders.Where(u => u.account == UserName && u.OrderDate.Year == Year && u.OrderDate.Month == Month).OrderBy(u => u.OrderID).Last().Account_RemainingBudget.ToString();
                Remaining = int.Parse(AccountBudget) - 1;

            }
            catch (Exception ex)
            {
                Remaining = BudgetPerMonth - 1;

            }
            var Status = "";
            try
            {
                var OrderHeader = new Orders
                {
                    OrderGUID = Guid.NewGuid(),
                    OrderDate = DateTime.Now,
                    user = UserName,
                    account = UserName,
                    TotalPrice = Order.Sum(u => (u.price * u.quantity)),
                    Account_RemainingBudget = Remaining
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

        public List<Orders> GetOrders(string UserEmail)
        {
            var data = _context.Orders.Where(u=> u.account == UserEmail).ToList();
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
