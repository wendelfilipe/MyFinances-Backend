using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace Backend.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AuthenticateService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var result = await signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
            return result.Succeeded;
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var aplicationUser = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await userManager.CreateAsync(aplicationUser, password);

            if(result.Succeeded)
            {
                await signInManager.SignInAsync(aplicationUser, isPersistent: false);
            }
            return result.Succeeded;
        }
    }
}