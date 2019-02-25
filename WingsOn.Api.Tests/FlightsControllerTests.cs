using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace WingsOn.Api.Tests
{
    public class FlightsControllerTests : TestBase
    {
        [Theory]
        [InlineData("GET")]
        public async Task GetFlights_WhenFlightsExist_ShouldReturn200StatusCode(string method)
        {
            // Arrange
            var request = CreateRequestMessage(method, "/api/flights");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetFlight_WhenFlightExist_ShouldReturn200StatusCode(string method)
        {
            // Arrange
            var request = CreateRequestMessage(method, "api/flights/30");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetFlight_WhenFlightWithSuchIdDoesNotExist_ShouldReturn404StatusCode(string method)
        {
            // Arrange
            var request = CreateRequestMessage(method, "api/persons/777");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetPassengersForFlight_WhenFlightNumberIsCorrectAndSuchFlightExists_ShoudReturn200StatucCode(string method)
        {
            // Arrange
            var request = CreateRequestMessage(method, "api/flights/PZ696/passengers");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetPassengersForFlight_WhenFlightWithSuchNumberDoesNotExist_ShoudReturn404StatucCode(string method)
        {
            // Arrange
            var request = CreateRequestMessage(method, "api/flights/QWEQRTY/passengers");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
