using TestWebApiIdentity.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApiIdentity.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {

    }
}