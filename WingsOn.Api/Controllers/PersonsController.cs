using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Bll.Dtos;
using WingsOn.Bll.Helpers;
using WingsOn.Bll.Interfaces;

namespace WingsOn.Api.Controllers
{
    /// <summary>Persons endpoint.</summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personsService;
        private readonly IBookingService _bookingService;
        private readonly IFlightService _flightService;


        public PersonsController(IPersonService personService, IBookingService bookingService, IFlightService flightService)
        {
            _personsService = personService;
            _bookingService = bookingService;
            _flightService = flightService;
        }

        /// <summary>
        /// Returns list of all persons. Can be specified with gender type.
        /// </summary>
        /// <param name="personResourceParameters">Person parameters to be specifying.</param>
        /// <returns>Returns list of all persons. If parameters were specified - returns filtered list.</returns>
        /// <response code="200">Successful response.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonDto>), 200)]
        public IActionResult GetPersons([FromQuery] PersonResourceParameters personResourceParameters)
        {
            var persons = _personsService.GetAll(personResourceParameters);
            
            return Ok(persons);
        }

        /// <summary>
        /// Returns person by id.
        /// </summary>
        /// <param name="id">Person id.</param>
        /// <returns>Returns person.</returns>
        /// <response code="200">Successful response.</response>
        /// <response code="404">Person wasn't found.</response>
        [HttpGet("{id}", Name = "GetPerson")]
        [ProducesResponseType(typeof(PersonDto), 200)]
        [ProducesResponseType(typeof(void), 404)]
        public IActionResult GetPerson(int id)
        {
            var person = _personsService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        /// <summary>
        /// Updates person.
        /// </summary>
        /// <param name="id">Person id.</param>
        /// <param name="personForUpdate">New person.</param>
        /// <returns>Returns no content.</returns>
        /// <response code="204">Person was updated successfully.</response>
        /// <response code="404">Person for updated wasn't found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 404)]
        public ActionResult UpdatePerson(int id, PersonDto personForUpdate)
        {
            var person = _personsService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            person = personForUpdate;
            _personsService.Save(person);

            return NoContent();
        }

        /// <summary>
        /// Updates person partially.
        /// </summary>
        /// <param name="id">Person id.</param>
        /// <param name="personPatch">List of operations for person updating.</param>
        /// <returns>Returns updated person.</returns>
        /// <response code="201">Person was updated successfully.</response>
        /// <response code="404">Person for update wasn't found.</response> 
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(PersonDto), 201)]
        [ProducesResponseType(typeof(void), 400)]
        public ActionResult UpdatePersonAddress(int id, [FromBody] JsonPatchDocument<PersonDto> personPatch)
        {
            var person = _personsService.Get(id);

            if (person == null)
            {
                return BadRequest();
            }

            personPatch.ApplyTo(person);

            _personsService.Save(person);

            return CreatedAtRoute("GetPerson", new {id = id}, person);
        }
    }
}
