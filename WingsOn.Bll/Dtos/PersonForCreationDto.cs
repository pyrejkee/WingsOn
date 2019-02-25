using System;
using System.ComponentModel.DataAnnotations;
using WingsOn.Domain;

namespace WingsOn.Bll.Dtos
{
    public class PersonForCreationDto
    {
        [Required(ErrorMessage = "You should fill out a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You should fill out a date of birth.")]
        public DateTime DateBirth { get; set; }

        [Required(ErrorMessage = "You should fill out a gender")]
        public GenderType Gender { get; set; }

        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; } 
    }
}
