using Microsoft.AspNetCore.Mvc;
using WingsOn.Bll.Dtos;
using WingsOn.Bll.Interfaces;

namespace WingsOn.Api.Controllers
{
    /// <summary>Bookings endpoint</summary>
    [Route("api/flights/{flightId}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IFlightService _flightService;

        public BookingsController(IBookingService bookingService, IFlightService flightService)
        {
            _bookingService = bookingService;
            _flightService = flightService;
        }

        /// <summary>
        /// Returns booking for specified flight.
        /// </summary>
        /// <param name="flightId">Flight id.</param>
        /// <param name="bookingId">Booking id.</param>
        /// <returns>Returns booking for specified flight.</returns>
        /// <response code="200">Successful response.</response>
        /// <response code="404">Booking with such id or such flight wasn't found.</response>
        [HttpGet("{bookingId}", Name = "GetBooking")]
        [ProducesResponseType(typeof(BookingDto), 200)]
        [ProducesResponseType(typeof(void), 404)]
        public ActionResult GetBooking(int flightId, int bookingId)
        {
            var flight = _flightService.Get(flightId);

            if (flight == null)
            {
                return NotFound();
            }

            var booking = _bookingService.Get(bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        /// <summary>
        /// Creates booking for specified flight.
        /// </summary>
        /// <param name="flightId">Flight id.</param>
        /// <param name="bookingForCreationDto">Booking for creation.</param>
        /// <returns>Returns newly created booking.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BookingDto), 201)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(void), 404)]
        public IActionResult CreateBooking(int flightId, BookingForCreationDto bookingForCreationDto)
        {
            if (bookingForCreationDto == null)
            {
                return BadRequest();
            }

            var flight = _flightService.Get(flightId);

            if (flight == null)
            {
                return NotFound();
            }

            bookingForCreationDto.Flight = flight;

            var createdBooking = _bookingService.CreateBooking(bookingForCreationDto);

            return CreatedAtRoute("GetBooking", new {flightId = createdBooking.Flight.Id, bookingId = createdBooking.Id}, createdBooking);
        }
    }
}
