using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiServerOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OneTestController : ControllerBase
    {
        [HttpGet("GetMethod1")]
        public string GetMethod1()
        {
            var a = HttpContext;
            var s = a.User.Claims.ToList()[6];
            return "OneTest GetMethod1";
        }
    }
}