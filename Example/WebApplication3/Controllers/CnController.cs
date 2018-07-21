using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ZhController : Controller
    {
        [HttpPost]
        public IActionResult Index()
        {
            return Content("hello");
        }

        [HttpPost("Informat")]
        public string Informat([FromBody] Informattor param)
        {
            return $"PcName:{param.PcName} UserName:{param.UserName} Version:{param.Version}";
        }
    }
}