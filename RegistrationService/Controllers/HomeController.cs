using Microsoft.AspNetCore.Mvc;

namespace RegistrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet("get-message")]
        public ActionResult<string> GetMessage()
        {
            return "Service1";
        }
    }
}
