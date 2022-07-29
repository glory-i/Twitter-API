using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.Authentication;
using TwitterAPI.Model;
using TwitterAPI.Services.AuthenticationServices.Interface;

namespace TwitterAPI.Services.AuthenticationServices.Implementation
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> roleManager;

        private ApplicationDbContext _context;
        public AuthenticationServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApplicationDbContext context)
        {
            this.userManager = userManager;
            _configuration = configuration;
            this.roleManager = roleManager;
            _context = context;
        }


        public async Task<Response> RegisterUser(RegisterModel model)
        {
            //throw new NotImplementedException();
            //check if there is a user w that username
            var userExists = await userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                return new Response { Status = "ERROR", Message = "User already exists" };

            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                //Username = model.Username,
                UserName = model.UserName
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new Response { Status = "ERROR", Message = "User not created" };

                //return Ok(new Response { Status = "Success", Message = "User Created Succesfully" });

            }

            //if there is no admin role create one, if there is no user role create one
            //if there is an admin role make the user an ordinary user
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }
            if (await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }

            Account account = new Account
            {
                Username = model.UserName,
                Password = model.Password,
                Email = model.Email,
                TwitterName = model.TwitterName,
                Bio = model.Bio,
                NoOfFollowers = 0,
                NoOfFollowing = 0,
                NoOfTweets = 0,
                Birthday = model.Birthday,
                DateCreated = DateTime.Now.Date
            };
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return new Response { Status = "Success", Message = "User Created Succesfully" };

        }
    }
}
