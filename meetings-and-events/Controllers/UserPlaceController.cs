using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace meetings_and_events.Controllers
{
    [Route("userplace")]
    [ApiController]
    public class UserPlaceController : Controller
    {
        [HttpGet, Route("get")]
        [Authorize]
        public JsonResult Get()
        {
            string message = "Dziala";
            return new JsonResult(message);
        }
    }
}