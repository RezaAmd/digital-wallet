using Application.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityApi.Areas.Identity.Controllers
{
    [ApiController]
    [Route("[area]/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        public async Task<ApiResult<object>> SignIn()
        {
            return Ok();
        }
    }
}
