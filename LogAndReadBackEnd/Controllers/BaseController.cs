namespace LogAndReadBackEnd.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        [ApiController]
        [Route("api/v1/[controller]")]
        public class BaseApiController : ControllerBase
        {
        }
    }
}