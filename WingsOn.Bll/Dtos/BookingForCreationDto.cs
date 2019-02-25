using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WingsOn.Bll.Dtos
{
    public class BookingForCreationDto
    {
        [Required(ErrorMessage = "You should fill out a number.")]
        public string Number { get; set; }

        public FlightDto Flight { get; set; }

        public PersonForCreationDto Customer { get; set; }

        public IEnumerable<PersonForCreationDto> Passengers { get; set; }
    }
}
