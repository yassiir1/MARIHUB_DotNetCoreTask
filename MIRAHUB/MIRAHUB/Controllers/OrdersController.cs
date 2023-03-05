using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIRAHUB.Models;
using MIRAHUB.Services;

namespace MIRAHUB.Controllers
{
    [Authorize(Roles = "USER")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MIRAHUBDb _context;
        private readonly IOrderServices OrdersServices;

        public OrdersController(MIRAHUBDb context, IOrderServices ordersServices)
        {
            _context = context;
            OrdersServices = ordersServices;
        }
        [HttpGet]
        [Route("ListOrders")]
        //[AllowAnonymous]
        public IActionResult GetOrders()
        {
            var data = OrdersServices.GetOrders();
            return Ok(new { data = data, Status = 200, Message = "Success" });
        }

        [HttpGet]
        [Route("GetOrderByID")]
        [AllowAnonymous]
        public IActionResult GetOrderBy(int id)
        {
            var order = OrdersServices.GetOrderByID(id);
            return Ok(new { data = order, Message = "Success", Status = 200 });
        }
        [HttpPost]
        [Route("AddOrder")]
        public IActionResult AddOrder(List<AddOrder> Order)
        {
            int Status;
            var UserEmail = User.Identity.Name;
            var data = OrdersServices.AddOrder(Order, UserEmail);
            if (data == "Success")
                Status = 200;
            else
                Status = 400;
            return Ok(new { Message = data, Status = Status });
        }
        [HttpPut]
        [Route("UpdateOrder")]
        public IActionResult PutOrder(int id, Orders Order)
        {
            if (id != Order.OrderID)
            {
                return BadRequest();
            }
            var data = OrdersServices.UpdateOrder(Order);
            if (data == "Success")
                return Ok(new { data = data, Status = 200 });
            else
                return BadRequest(new { Message = data, Status = 400 });
        }
        [HttpDelete]
        [Route("DeleteOrder")]
        public IActionResult DeleteOrder(int id)
        {
            var Message = "";
            var Status = 0;
            var data = OrdersServices.DeleteOrder(id);
            if (data == "Success")
            {
                Message = "Order Deleted Successfully";
                Status = 200;
            }

            else
            {
                Message = "Order Didn't Deleted";
                Status = 400;
            }
            return Ok(new { Message = Message, Status = Status });
        }
    }
}
