namespace ShishaFlavours.API.Common.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Identity;
    using ShishaFlavoursAPI.Models;
    using System.Threading.Tasks;

    public static class AuthenticateExtension
    {
        public static async Task<User> Authenticate(this SignInManager<User> signInManager, string Username, string Password)
        {
            User user = await signInManager.UserManager.FindByNameAsync(Username);
            SignInResult result = await signInManager.CheckPasswordSignInAsync(user, Password, false);

            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
