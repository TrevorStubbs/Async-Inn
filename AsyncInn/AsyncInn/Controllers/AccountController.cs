using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncInn.Models;
using AsyncInn.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // api/account/register
        [HttpPost,Route("register")]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            // do something to put this into the database
            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                UserName = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName
            };

            //create the user
            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                // sign the user in if it was successful.
                await _signInManager.SignInAsync(user, false);

                return Ok();

            }
            return BadRequest("Invalid Registration");

        }
    }
}
