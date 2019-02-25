using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WingsOn.Domain
{
    public class Flight : DomainObject
    {
        public string Number { get; set; }

        public Airline Carrier { get; set; }

        public Airport DepartureAirport { get; set; }

        public DateTime DepartureDate { get; set; }

        public Airport ArrivalAirport { get; set; }

        public DateTime ArrivalDate { get; set; }

        public decimal Price { get; set; }
    }
}
