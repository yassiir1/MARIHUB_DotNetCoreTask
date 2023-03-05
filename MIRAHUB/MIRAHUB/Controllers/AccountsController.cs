using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using MIRAHUB.Models;
using MIRAHUB.Services;
//using MIRAHUB.Cache;
namespace MIRAHUB.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "ADMIN")]
    public class AccountsController : ControllerBase
    {
        private readonly MIRAHUBDb _context;
        private readonly IAccountsServices AccountServices;

        public AccountsController(MIRAHUBDb context, IAccountsServices accountServices)
        {
            _context = context;
            AccountServices = accountServices;
        }

        [HttpGet]
        [Route("ListAccounts")]
        public IActionResult GetAccounts()
        {
            var data = AccountServices.GetAccounts();
            return Ok(new { data = data, Status = 200, Message = "Success" });
        }

        [HttpGet]
        [Route("ListAccountByID")]
        public IActionResult GetAccountBy(int id)
        {
            var account = AccountServices.GetAccountBy(id);
            return Ok(new { data = account, Message = "Success", Status = 200 });
        }

        [HttpPut]
        [Route("UpdateAccount")]
        public IActionResult PutAccounts( int id, Accounts accounts)
        {
            if (id != accounts.Id)
            {
                return BadRequest();
            }
            var data = AccountServices.UpdateAccount(accounts);
            if (data == "Success")
                return Ok(new { data = data, Status = 200 });
            else
                return BadRequest(new {Message = data, Status = 400 });
        }

  
        [HttpPost]
        [Route("AddAccount")]
        public IActionResult PostAccounts([FromHeader] string phoneNumber,string emailAccount, string password /*Accounts accounts*/)
        {
            Accounts Acc = new Accounts()
            {
                Password = password,
                PhoneNumber = phoneNumber,
                EmailAccount = emailAccount,
            };
            var data = AccountServices.AddAccount(Acc);
            if (data == "Success")
                return Ok(new { Message = data, Status = 200 });
            else
                return Ok(new { Message = data, Status = 400 });
        }

        [HttpDelete]
        [Route("DeleteAccount")]
        public IActionResult DeleteAccounts(int id)
        {
            var Message = "";
            var Status = 0;
            var data = AccountServices.DeleteAccount(id);
            if (data == "Success")
            {
                Message = "Account Deleted Successfully";
                Status = 200;
            }

            else
            {
                Message = "Account Didn't Deleted";
                Status = 400;
            }
            return Ok(new { Message  = Message, Status = Status});
        }

        private bool AccountsExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
