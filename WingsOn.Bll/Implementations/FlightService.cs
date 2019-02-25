using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WingsOn.Bll.Dtos;
using WingsOn.Bll.Interfaces;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Bll.Implementations
{
    public class FlightService : IFlightService
    {
        private readonly IRepository<Flight> _repository;
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IMapper _mapper;

        public FlightService(IRepository<Flight> repository, IRepository<Booking> bookingRepository, IMapper mapper)
        {
            _repository = repository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public IEnumerable<Person> GetAllPersonsByFlightNumber(string flightNumber)
        {
            return _bookingRepository.GetAll().FirstOrDefault(f => f.Flight.Number == flightNumber)?.Passengers;
        }

        public IEnumerable<FlightDto> GetAll()
        {
            var flightsFromRepo = _repository.GetAll();
            var fligthsToReturn = _mapper.Map<IEnumerable<Flight>, IEnumerable<FlightDto>>(flightsFromRepo);

            return fligthsToReturn;
        }

        public FlightDto Get(int id)
        {
            var flightFromRepo = _repository.Get(id);
            var flightToReturn = _mapper.Map<Flight, FlightDto>(flightFromRepo);

            return flightToReturn;
        }

        public void Save(FlightDto flightDto)
        {
            var flightToRepo = _mapper.Map<FlightDto, Flight>(flightDto);
            _repository.Save(flightToRepo);
        }

        public FlightDto GetFlightByNumber(string number)
        {
            var flight = _repository.GetAll().FirstOrDefault(f => f.Number == number);
            var flightToReturn = _mapper.Map<Flight, FlightDto>(flight);

            return flightToReturn;
        }
    }
}
