using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NavTechSignalR_Server.Services;
using System.Xml.Linq;

namespace NavTechSignalR_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public MessageController(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("StartConnection")]
        public IActionResult StartConnection()
        {
            ConnectionService connectionService = new ConnectionService();
            connectionService.Connect();
            return Ok();
        }

        [HttpPost]
        [Route("PostXml")]
        public async Task<IActionResult> PostXml([FromQuery] string value)
        {
            //string message = DecodeProtocolService.DecodeXML(value);
            string message = "1234";
            await _hubContext.Clients.All.SendAsync("message", $"{message}");
            return Ok();
        }
    }
}
