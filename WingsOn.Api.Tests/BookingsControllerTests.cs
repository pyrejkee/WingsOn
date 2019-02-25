using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WingsOn.Bll.Dtos;
using WingsOn.Domain;
using Xunit;

namespace WingsOn.Api.Tests
{
    public class BookingsControllerTests : TestBase
    {
        [Theory]
        [InlineData("GET")]
        public async Task GetBooking_WhenFlightExistsAndBookingExists_ShoudReturn200StatusCode(string method)
        {
            // Arrange
            var request = CreateRequestMessage(method, "api/flights/81/bookings/83");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetBooking_WhenFlightAndBookingAreNotCorrect_ShoudReturn400StatusCode(string method)
        {
            // Arrange
            var request = CreateRequestMessage(method, "api/flights/777/bookings/888");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("POST")]
        public async Task CreateBooking_WhenBookingSpecifiedCorrectly_ShoudReturn201StatusCode(string method)
        {
            // Arrange
            var requestObj = new BookingForCreationDto
            {
                Number = "WO-789654",
                Customer = new PersonForCreationDto
                {
                    Name = "Test Customer",
                    Address = "Test address",
                    DateBirth = new DateTime(1993, 07, 27),
                    Email = "test@gmail.com",
                    Gender = GenderType.Male
                },
                Passengers = new List<PersonForCreationDto>
                {
                    new PersonForCreationDto
                    {
                        Name = "Test Customer2",
                        Address = "Test address2",
                        DateBirth = new DateTime(1994, 04, 24),
                        Email = "test2@gmail.com",
                        Gender = GenderType.Male
                    },
                    new PersonForCreationDto
                    {
                        Name = "Test Customer3",
                        Address = "Test address3",
                        DateBirth = new DateTime(1995, 05, 25),
                        Email = "test3@gmail.com",
                        Gender = GenderType.Female
                    }
                }
            };

            var request = CreateRequestMessage(method, "api/flights/81/bookings", requestObj);

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("POST")]
        public async Task CreateBooking_WhenBookingIsNotSpecifiedCorrectly_ShoudReturn400StatusCode(string method)
        {
            // Arrange
            var requestObj = new BookingForCreationDto
            {
                
            };

            var request = CreateRequestMessage(method, "api/flights/81/bookings", requestObj);

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("POST")]
        public async Task CreateBooking_WhenFlightWithSuchIdDoesNotExists_ShoudReturn404StatusCode(string method)
        {
            // Arrange
            var requestObj = new BookingForCreationDto
            {
                Number = "WO-789654",
                Customer = new PersonForCreationDto
                {
                    Name = "Test Customer",
                    Address = "Test address",
                    DateBirth = new DateTime(1993, 07, 27),
                    Email = "test@gmail.com",
                    Gender = GenderType.Male
                },
                Passengers = new List<PersonForCreationDto>
                {
                    new PersonForCreationDto
                    {
                        Name = "Test Customer2",
                        Address = "Test address2",
                        DateBirth = new DateTime(1994, 04, 24),
                        Email = "test2@gmail.com",
                        Gender = GenderType.Male
                    },
                    new PersonForCreationDto
                    {
                        Name = "Test Customer3",
                        Address = "Test address3",
                        DateBirth = new DateTime(1995, 05, 25),
                        Email = "test3@gmail.com",
                        Gender = GenderType.Female
                    }
                }
            };
            var request = CreateRequestMessage(method, "api/flights/777/bookings", requestObj);

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
