using KafkaPlayground.Producer;
using Microsoft.AspNetCore.Mvc;

namespace KafkaPlayground.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IEnumerable<IProducer> _producers;

        public MessagesController(IEnumerable<IProducer> producers)
        {
            _producers = producers;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] string message)
        {
            foreach (var producer in _producers)
            {
                await producer.SendMessage(message);
            }

            return Ok();
        }
    }
}
