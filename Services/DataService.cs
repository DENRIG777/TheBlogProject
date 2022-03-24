using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext, 
                RoleManager<IdentityRole> roleManager, 
                UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            //Create the DB from the Migrations
            await _dbContext.Database.MigrateAsync();
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

            await _userManager.CreateAsync(adminUser, "soYouwant@2");

            //Add this new user to the Administrater role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());    

            //Create an instance of moderator user
            var modUser = new BlogUser()
            {
                Email = "denrig777@gmail.com",
                UserName = "Denrig777@gmail.com",
                FirstName = "Den",
                LastName = "Rig",
                EmailConfirmed = true,
            };
            //Uses the UserManager to create a new user that is defined by modUser

            await _userManager.CreateAsync(modUser, "soYouwant@2");

            //Add this new user to the  Moderator role

            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());

            //Create an instance of GuestAuthor user
            // var guestUser = new BlogUser()
            //{
            // Email = "denrig777@gmail.com",
            // UserName = "Denrig777@gmail.com",
            // FirstName = "Den",
            // LastName = "Rig",
            // EmailConfirmed = true,
            // };
            //Uses the UserManager to create a new user that is defined by guestUser

            // await _userManager.CreateAsync(guestUser, "123@Abc");

            //Add this new user to the GuestAuthor role

            // await _userManager.AddToRoleAsync(guestUser, BlogRole.GuestAuthor.ToString());

        }





    }
}
