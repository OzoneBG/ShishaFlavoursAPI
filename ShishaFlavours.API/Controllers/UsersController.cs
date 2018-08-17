namespace ShishaFlavours.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using ShishaFlavours.API.Common.Infrastructure.Extensions;
    using ShishaFlavours.API.RequestModels.User;
    using ShishaFlavours.Services.ResponseModels;
    using ShishaFlavoursAPI.Models;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class UsersController : Controller
    {
        private IConfiguration config = null;
        private UserManager<User> userManager = null;
        private SignInManager<User> signInManager = null;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel userData)
        {
            IActionResult result = null;

            if(ModelState.IsValid)
            {
                if(userData.IsConsent)
                {
                    User user = new User
                    {
                        UserName = userData.Username,
                        Email = userData.Email
                    };

                    IdentityResult userResult = await userManager.CreateAsync(user, userData.Password);
                    if (userResult.Succeeded)
                    {
                        ResultStatus status = new ResultStatus()
                        {
                            Status = true,
                            Message = "The user was successfully created."
                        };
                        result = new JsonResult(status);
                    }
                    else
                    {
                        result = new JsonResult(new { ErrorMessage = "The user failed to create. Please check errors.", userResult.Errors });
                    }
                } 
                else
                {
                    ResultStatus status = new ResultStatus()
                    {
                        Status = false,
                        Message = "The user is not consent."
                    };

                    result = new JsonResult(status);
                }
                
            } else
            {
                ResultStatus status = new ResultStatus()
                {
                    Status = false,
                    Message = "The input data was invalid"
                };

                result = new JsonResult(status);
            }

            return result;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel changePasswordData)
        {
            IActionResult response = null;

            if(ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(changePasswordData.Username);
                IdentityResult userResult = await userManager.ChangePasswordAsync(user, changePasswordData.CurrentPassword, changePasswordData.NewPassword);

                if(userResult.Succeeded)
                {
                    response = new JsonResult("Successfully changed password");
                }
                else
                {
                    response = new JsonResult(new { Message = "Failed to change password", userResult.Errors });
                }
            }
            else
            {
                response = new JsonResult("The input data was invalid");
            }

            return response;
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                ResultStatus status = new ResultStatus()
                {
                    Status = false,
                    Message = "Id is null or empty"
                };

                return new JsonResult(status);
            }
            else
            {
                User user = await userManager.FindByIdAsync(userId);
                IdentityResult result = await userManager.DeleteAsync(user);

                if(result.Succeeded)
                {
                    ResultStatus status = new ResultStatus()
                    {
                        Status = true,
                        Message = "User successfully deleted"
                    };

                    return new JsonResult(status);
                }
                else
                {
                    return new JsonResult(result.Errors);
                }
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            IActionResult response = Unauthorized();

            if(ModelState.IsValid)
            {
                var user = await signInManager.Authenticate(login.Username, login.Password);

                if (user != null)
                {
                    var tokenString = BuildToken(user);
                    response = Ok(new { token = tokenString });
                }
            }

            return response;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser(string name)
        {
            if (User.Identity.IsAuthenticated)
            {
                User userInDB = await userManager.FindByNameAsync(name);

                if (userInDB == null)
                {
                    return NotFound("Didn't find user");
                }

                return new JsonResult(userInDB);
            }
            else
            {
                return Unauthorized();
            }
        }

        private string BuildToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                 config["Jwt:Issuer"],
                 claims,
                 expires: DateTime.Now.AddMinutes(30),
                 signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}