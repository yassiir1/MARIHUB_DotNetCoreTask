using MIRAHUB.Models;

namespace MIRAHUB.Services
{
    public interface IOrderServices
    {
        public List<Orders> GetOrders();
        public Orders GetOrderByID(int id);
        public string AddOrder(List<AddOrder> Order, string UserEmail);
        public string UpdateOrder(Orders Order);
        public string DeleteOrder(int id);
    }
}
