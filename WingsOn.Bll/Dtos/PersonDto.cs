using System;
using WingsOn.Domain;

namespace WingsOn.Bll.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateBirth { get; set; }

        public GenderType Gender { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
    }
}
