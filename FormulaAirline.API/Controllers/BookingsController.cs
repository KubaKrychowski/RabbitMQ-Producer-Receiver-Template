using FormulaAirline.API.Models;
using FormulaAirline.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMessageProducerService _messageProducerService;
        public BookingsController(
            ILogger<WeatherForecastController> logger,
            IMessageProducerService messageProducerService)
        {
            _logger = logger;
            _messageProducerService = messageProducerService;
        }

        // In-Memory db
        public static readonly List<Booking> _bookings = new();

        [HttpPost]
        public IActionResult CreateBooking(Booking book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            _bookings.Add(book);

            _messageProducerService.SendingMessage<Booking>(book);

            return Ok();
        }
    }
}
