using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Bll.Dtos;
using WingsOn.Bll.Interfaces;

namespace WingsOn.Api.Controllers
{
    /// <summary>Flights endpoint.</summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IPersonService _personService;

        public FlightsController(IFlightService flightService, IPersonService personService)
        {
            _flightService = flightService;
            _personService = personService;
        }

        /// <summary>
        /// Returns list of all flights.
        /// </summary>
        /// <returns>Returns list of all flights.</returns>
        /// <response code="200">Successful response.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FlightDto>), 200)]
        public IActionResult GetFlights()
        {
            var flights = _flightService.GetAll();

            return Ok(flights);
        }

        /// <summary>
        /// Returns flight by id.
        /// </summary>
        /// <param name="id">Flight id.</param>
        /// <returns>Returns flight.</returns>
        /// <response code="200">Successful response.</response>
        /// <response code="404">Flight wasn't found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FlightDto), 200)]
        [ProducesResponseType(typeof(void), 404)]
        public ActionResult GetFlight(int id)
        {
            var flight = _flightService.Get(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        /// <summary>
        /// Returns list of all passengers for specified flight.
        /// </summary>
        /// <param name="flightNumber">Flight number.</param>
        /// <returns>List of all passenger for specified flight.</returns>
        /// <response code="200">Successful response.</response>
        /// <response code="400">Flight number wasn't specified.</response>
        /// <response code="404">Flight with specified number wasn't found.</response>
        [HttpGet("{flightNumber}/passengers")]
        [ProducesResponseType(typeof(IEnumerable<PersonDto>), 200)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(void), 404)]
        public ActionResult GetPassengersForFlight(string flightNumber)
        {
            if (flightNumber == null)
            {
                return BadRequest();
            }

            var flight = _flightService.GetFlightByNumber(flightNumber);

            if (flight == null)
            {
                return NotFound();
            }

            var passengers = _flightService.GetAllPersonsByFlightNumber(flightNumber);

            return Ok(passengers);
        }
    }
}
