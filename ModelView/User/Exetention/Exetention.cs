using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Models;
using ModelView;

namespace ModelView
{
    public static class Exetention
    {
        public static User MapFromRegisterToUser(this UserRegisterViewModel user ) 
        {
            return new User
            {
                Email = user.Email,
                UserName = user.UserName,
                Name = user.UserName
            };
        }

        public static User MapFromLoginToUser(this UserLoginViewModel user)
        {
            return new User
            {
                UserName = user.Login.Contains("@") ? null : user.Login,
                Email = user.Login.Contains("@") ? user.Login : null,
            };
        }
    }
}
