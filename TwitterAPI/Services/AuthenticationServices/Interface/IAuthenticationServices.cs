using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.Authentication;

namespace TwitterAPI.Services.AuthenticationServices.Interface
{
    public interface IAuthenticationServices
    {
        public Task<Response> RegisterUser(RegisterModel model);
        //public Task<IActionResult> Login([FromBody] LoginModel model);
    }
}
