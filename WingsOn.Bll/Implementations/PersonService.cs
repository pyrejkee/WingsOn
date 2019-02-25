using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WingsOn.Bll.Dtos;
using WingsOn.Bll.Helpers;
using WingsOn.Bll.Interfaces;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Bll.Implementations
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> _repository;
        private readonly IMapper _mapper;

        public PersonService(IRepository<Person> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<PersonDto> GetAll(PersonResourceParameters personResourceParameters)
        {
            if (personResourceParameters.Gender != null)
            {
                var personsFromRepo = _repository.GetAll().Where(p => p.Gender == personResourceParameters.Gender);
                var personsToReturn = _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(personsFromRepo);
                return personsToReturn;
            }

            return GetAll();
        }

        public IEnumerable<PersonDto> GetAll()
        {
            var personsFromRepo = _repository.GetAll();
            var personsToReturn = _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(personsFromRepo);

            return personsToReturn;
        }

        public PersonDto Get(int id)
        {
            var personFromRepo = _repository.Get(id);
            var personToReturn = _mapper.Map<Person, PersonDto>(personFromRepo);

            return personToReturn;
        }

        public void Save(PersonDto personDto)
        {
            var personToRepo = _mapper.Map<PersonDto, Person>(personDto);
            _repository.Save(personToRepo);
        }
    }
}
