using Microsoft.AspNetCore.Mvc;
using NavTechSignalR_Server.Services;

namespace NavTechSignalR_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        //for blazor the start the connection
        [HttpGet]
        [Route("StartConnection")]
        public IActionResult StartConnection()
        {
            ConnectionService connectionService = new ConnectionService();
            connectionService.Connect();
            return Ok();
        }
    }
}
