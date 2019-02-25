using System.Collections.Generic;
using WingsOn.Bll.Dtos;
using WingsOn.Domain;

namespace WingsOn.Bll.Interfaces
{
    public interface IFlightService
    {
        FlightDto GetFlightByNumber(string number);

        IEnumerable<Person> GetAllPersonsByFlightNumber(string flightNumber);

        IEnumerable<FlightDto> GetAll();

        FlightDto Get(int id);

        void Save(FlightDto flightDto);
    }
}
