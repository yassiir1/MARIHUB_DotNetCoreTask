//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using MIRAHUB.Models;
using System.Security.Claims;

namespace MIRAHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUser : ControllerBase
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> _user;
        public RegisterUser(SignInManager<AppUser> signInManager, UserManager<AppUser> user)
        {
            this.signInManager = signInManager;
            this._user = user;
        }
        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult> PostAccounts(AppUser UserModel)
        {
            //var existUser = await _user.FindByEmailAsync(UserModel.Email);
            //var Message = "";
            //if (existUser == null)
            //{
            //    Message = "The Email Is Already Exist!";
            //    return BadRequest(new { StatusCode = 500, Message = Message });
            //}
            ///////////////////////////
            // Create Admin User 
            ///////////////////////////

            //var Adminuser = new AppUser
            //{
            //    Name = "Yasser Hammad",
            //    Email = "yasser10@gmail.com",
            //    UserName = "yasser10@gmail.com",
            //    PhoneNumber = "01015490078"

            //};
            //var Adminresult = await _user.CreateAsync(Adminuser, "Yasser@123456");
            //await _user.AddToRoleAsync(Adminuser, "Admin");
            //return Ok(new { Message = "Admin User Created Successfully", Status = 200, data = Adminuser });



            ///////////////////////////
            // Create Normal User 
            ///////////////////////////


            //var user = new AppUser
            //{
            //    Name = "Yasser Hammad",
            //    Email = "yasser10@yahoo.com",
            //    UserName = "yasser10@yahoo.com",
            //    PhoneNumber = "01015490078"

            //};
            //var Userresult = await _user.CreateAsync(user, "Yasser#123456");
            //await _user.AddToRoleAsync(user, "User");
            //return Ok(new { Message = "Normal User Created Successfully", Status = 200, data = user });

            ///////////////////////////
            // Create Orders User 
            ///////////////////////////

            var Orderuser = new AppUser
            {
                Name = "Yasser Mohamed Hammad",
                Email = "yasseiirMohamed@yahoo.com",
                UserName = "yasseiirMohamed@yahoo.com",
                PhoneNumber = "01015490078"

            };
            var OrderUserRes = await _user.CreateAsync(Orderuser, "Yaser#1234567");
            await _user.AddToRoleAsync(Orderuser, "USERORDERS");
            return Ok(new { Message = "Order User Created Successfully", Status = 200, data = Orderuser });
        }
        [HttpPost]
        [Route("LogIn")]

        public async Task<ActionResult> Login(Login login)
        {
            var existUser = await _user.FindByEmailAsync(login.Email);
            var Message = "";
            if (existUser == null)
            {
                Message = "The Email Or Password Is Incorrect!";
                return BadRequest(new { StatusCode = 500, Message = Message });
            }
            else
            {
                var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, true, true);
                if (result.Succeeded)
                {
                    Message = "Successfully Logged In";
                    return Ok(new { StatusCode = 200, Message = Message, data = result });

                }
                else
                {
                    Message = "Email Or Password Is Incorrect";
                    return BadRequest(new { StatusCode = 400, Message = Message });
                }
            }
        }
    }
}
