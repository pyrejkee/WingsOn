using System.Collections.Generic;
using WingsOn.Bll.Dtos;

namespace WingsOn.Bll.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<BookingDto> GetAll();

        BookingDto Get(int id);

        void Save(BookingDto bookingDto);

        BookingDto CreateBooking(BookingForCreationDto bookingForCreationDto);
    }
}
