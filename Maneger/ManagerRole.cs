
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Migrations;
using ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maneger
{
    public class ManagerRole : MainManeger<IdentityRole>
    {
        public RoleManager<IdentityRole> roleManager { get; set; }
        public ManagerRole(RoleManager<IdentityRole> _roleManager,MyDBContext myDB):base(myDB)
        {
            roleManager = _roleManager;
        }

        public async Task<IdentityResult> Add(RoleViewModel roleViewModel)
        {
           return await roleManager.CreateAsync(new IdentityRole { Name = roleViewModel.Name});
        }

    }
}
