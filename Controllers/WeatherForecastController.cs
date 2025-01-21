using Microsoft.AspNetCore.Mvc;

namespace Mercuryfire_BackendExam_APIServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public IActionResult WebHook() => Ok(new { message = "Ok!" });

    }
}
