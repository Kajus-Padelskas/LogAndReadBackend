using Microsoft.AspNetCore.Mvc;

namespace LogAndReadBackEnd.Controllers
{
    public class BaseController : Controller
    {
        [ApiController]
        [Route("api/v1/[controller]")]
        public class BaseApiController : ControllerBase
        {

        }
    }
}
