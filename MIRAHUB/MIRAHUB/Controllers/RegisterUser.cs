//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MIRAHUB.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MIRAHUB.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUser : ControllerBase
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> _user;
        private readonly Jwt _JWTSetting;
        public RegisterUser(SignInManager<AppUser> signInManager, UserManager<AppUser> user, IOptions<Jwt> options)
        {
            this.signInManager = signInManager;
            this._user = user;
            this._JWTSetting = options.Value;
        }
        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult> PostAccounts(AppUser UserModel)
        {
            var existUser = await _user.FindByEmailAsync(UserModel.Email);
            var Message = "";
            if (existUser != null)
            {
                Message = "The Email Is Already Exist!";
                return BadRequest(new { StatusCode = 500, Message = Message });
            }
            ///////////////////////////
            // Create Admin User 
            ///////////////////////////

            //var Adminuser = new AppUser
            //{
            //    Name = "Yasser Hammad",
            //    Email = "yasser10@Admin.com",
            //    UserName = "yasser10@Admin.com",
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
            //    Email = "yasser10@User.com",
            //    UserName = "yasser10@USER.com",
            //    PhoneNumber = "01015490078"

            //};
            //var Userresult = await _user.CreateAsync(user, "Yasser#123456");
            //await _user.AddToRoleAsync(user, "USER");
            //return Ok(new { Message = "Normal User Created Successfully", Status = 200, data = user })

            ///////////////////////////
            // Create See Only Orders User 
            ///////////////////////////

            var Orderuser = new AppUser
            {
                Name = UserModel.Name,
                Email = UserModel.Email,
                UserName = UserModel.UserName,
                PhoneNumber = UserModel.PhoneNumber

            };
            var OrderUserRes = await _user.CreateAsync(Orderuser, "Ahmed#1234567");
            await _user.AddToRoleAsync(Orderuser, "USER");
            return Ok(new { Message = "Order User Created Successfully", Status = 200, data = Orderuser });
        }
        [HttpPost]
        [AllowAnonymous]
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
                var GetRole = await _user.GetRolesAsync(existUser);
                var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, true, true);
                List<Claim> claims = new List<Claim>
                {
                        new Claim("UserEmail", existUser.Email.ToString())
                };

                if (result.Succeeded)
                {
                    var TokenHandeler = new JwtSecurityTokenHandler();
                    var TokenKey = Encoding.UTF8.GetBytes(_JWTSetting.Secret);
                    var TokenDescriber = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(
                            new Claim[]
                            {
                                new Claim(ClaimTypes.Name,existUser.Id),
                                new Claim(ClaimTypes.Role,GetRole[0])
                            }
                          ),
                        Expires = DateTime.Now.AddHours(2),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(TokenKey),SecurityAlgorithms.HmacSha256)
                    };
                    var Token = TokenHandeler.CreateToken(TokenDescriber);
                    var UserToken = TokenHandeler.WriteToken(Token);
                    Message = "Successfully Logged In";
                    return Ok(new { StatusCode = 200, Message = Message, data = login, Token = UserToken });
                }
                else
                {
                    Message = "Email Or Password Is Incorrect";
                    return BadRequest(new { StatusCode = 400, Message = Message });
                }
            }
        }
        [HttpPost]
        [Route("ListUsers")]
        public async Task<ActionResult> ListUsers()
        {
            var data = await _user.Users.ToListAsync();
            return Ok(new { Message = "Success", Status = 200, data = data });

        }
    }
}
