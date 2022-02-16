using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheBlogProject.Data;
using TheBlogProject.Enums;
using TheBlogProject.Models;

namespace TheBlogProject.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        public async Task ManageDataAsync()
        {
            //Use 1: seed a few roles unto the system
            await SeedRolesAsync();
            //Use 2: seed a few users into the system
            await SeedUsersAsync();
        }

        //use 1
        private async Task SeedRolesAsync()
        {
            //If there are already Roles in the system, do nothing.
            if (_dbContext.Roles.Any())
            {
                return;
            }
            //Otherwise we want to create a few Roles.
            foreach(var role in Enum.GetNames(typeof(BlogRole)))
            {
                //Use the role manager to create roles
                await _roleManager.CreateAsync(new IdentityRole(role));
            }


        }

        //use2
        private async Task SeedUsersAsync()
        {
            //If there are already users in the system, do nothing.
            if(_dbContext.Users.Any())
            {
                return ;
            }
            //Creates a new instance of BlogUser
            var adminUser = new BlogUser()
            {
                Email = "dcoder789@gmail.com",
                UserName = "Dcoder789@gmail.com",
                FirstName = "Dennis",
                LastName = "Riggin",
                EmailConfirmed = true,
            };
            //Uses the UserManager to create a new user that is defined by adminUser
        }





    }
}
