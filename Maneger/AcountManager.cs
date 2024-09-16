using Microsoft.AspNetCore.Identity;
using Models;
using ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maneger
{
    public class AcountManager {

        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }

        public AcountManager(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

       

        public async Task<SignInResult> Login (UserLoginViewModel user){

            var u = await UserManager.FindByEmailAsync(user.Login);

            if (u == null)
                u = await UserManager.FindByNameAsync(user.Login);

            return await SignInManager.PasswordSignInAsync(
                    u,
                    user.Password,
                    user.RemeberMe,
                    true
            );
        }
        
        public async Task<IdentityResult> Register (UserRegisterViewModel user)
        {
            var u = user.MapFromRegisterToUser();
            var res = await UserManager.CreateAsync(u, user.Password);
            //await UserManager.AddToRoleAsync(u, "Admin");
            return res;

        }

        public async void LogOut() 
        {
            await SignInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePassword(UserChangePassword viewModel)
        {
            var User = await UserManager.FindByIdAsync(viewModel.UserID);

            return await UserManager.ChangePasswordAsync(User, viewModel.CurrentPassword, viewModel.NewPassword);
        }
        public async Task<string> GetResetPasswordCode(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                return string.Empty;
            }
            else
            {
                return await UserManager.GeneratePasswordResetTokenAsync(user);
            }
        }

        public async Task<IdentityResult> ResetPassword(UserResetPasswordViewModel viewModel)
        {
            var user = await UserManager.FindByEmailAsync(viewModel.Email);
            if (user != null)
            {
                return await UserManager.ResetPasswordAsync
                     (user, viewModel.Code, viewModel.NewPassword);
            }
            else
            {
                return IdentityResult.Failed(
                    new IdentityError()
                    {
                        Description = "Reset Password Is valid"
                    });
            }
        }


    }
}
