using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiServerTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TwoTestController : ControllerBase
    {
        [HttpGet("GetMethod1")]
        public string GetMethod1()
        {
            return "TwoTest GetMethod1";
        }
    }
}