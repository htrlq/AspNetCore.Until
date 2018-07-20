using AspNetCore.Until;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ValidBaseController
    {
        [HttpPost]
        public IActionResult Index(Informattor param)
        {
            return Content($"PcName:{param.PcName} UserName:{param.UserName} Version:{param.Version}");
        }

        [HttpPost("Check")]
        public IActionResult Check(InformattorStamp param)
        {
            return Content($"PcName:{param.PcName} UserName:{param.UserName} Version:{param.Version}");
        }
    }
}