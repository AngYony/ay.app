using Microsoft.AspNetCore.Mvc;
using WY.Domain.Abstractions.Events;

namespace MediatR.Sample.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEventBus eventBus;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IEventBus eventBus)
        {
            _logger = logger;
            this.eventBus = eventBus;
        }

        [HttpGet(Name = "wy")]
        public async Task Get(CancellationToken cancellationToken)
        {
           await eventBus.PublishEventAsync(new WySampleEvent("ÕÅÈý", 22), cancellationToken);

        }
    }
}
