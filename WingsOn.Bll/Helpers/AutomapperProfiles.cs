using AutoMapper;
using WingsOn.Bll.Dtos;
using WingsOn.Domain;

namespace WingsOn.Bll.Helpers
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<BookingForCreationDto, Booking>();
            CreateMap<Booking, BookingDto>();
            CreateMap<PersonForCreationDto, Person>();
            CreateMap<FlightDto, Flight>();
            CreateMap<AirportDto, Airport>();
            CreateMap<AirlineDto, Airline>();
        }
    }
}
