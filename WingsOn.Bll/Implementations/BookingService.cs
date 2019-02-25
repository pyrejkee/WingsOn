using System;
using System.Collections.Generic;
using AutoMapper;
using WingsOn.Bll.Dtos;
using WingsOn.Bll.Interfaces;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Bll.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _repository;
        private readonly IRepository<Person> _personRepository;
        private readonly IMapper _mapper;
        private readonly Random _random = new Random();

        public BookingService(IRepository<Booking> repository, IRepository<Person> personRepository, IMapper mapper)
        {
            _repository = repository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public IEnumerable<BookingDto> GetAll()
        {
            var bookingsFromRepo = _repository.GetAll();
            var bookingsToReturn = _mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDto>>(bookingsFromRepo);

            return bookingsToReturn;
        }

        public BookingDto Get(int id)
        {
            var bookingFromRepo = _repository.Get(id);
            var bookingToReturn = _mapper.Map<Booking, BookingDto>(bookingFromRepo);

            return bookingToReturn;
        }

        public void Save(BookingDto bookingDto)
        {
            var bookingToRepo = _mapper.Map<BookingDto, Booking>(bookingDto);
            _repository.Save(bookingToRepo);
        }

        public BookingDto CreateBooking(BookingForCreationDto bookingForCreationDto)
        {
            var bookingToRepo = _mapper.Map<BookingForCreationDto, Booking>(bookingForCreationDto);
            bookingToRepo.Id = GenerateId();

            CreatePersonForBooking(bookingToRepo);

            _repository.Save(bookingToRepo);

            var bookingToReturn = _mapper.Map<Booking, BookingDto>(bookingToRepo);

            return bookingToReturn;
        }

        private void CreatePersonForBooking(Booking bookingToRepo)
        {
            bookingToRepo.Customer.Id = GenerateId();
            _personRepository.Save(bookingToRepo.Customer);

            foreach (var passenger in bookingToRepo.Passengers)
            {
                passenger.Id = GenerateId();
                _personRepository.Save(passenger);
            }
        }

        // as we're working with in-memory databse so generate ids manually
        private int GenerateId()
        {
            return _random.Next(100, 1000000);
        }
    }
}
