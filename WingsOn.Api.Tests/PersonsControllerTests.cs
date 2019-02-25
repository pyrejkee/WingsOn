using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json.Serialization;
using WingsOn.Bll.Dtos;
using WingsOn.Domain;
using Xunit;

namespace WingsOn.Api.Tests
{
    public class PersonsControllerTests : TestBase
    {

        [Theory]
        [InlineData("GET")]
        public async Task GetPersons_WhenPersonsExist_ShouldReturn200StatusCode(string method)
        {
            // Arrange
            var request = CreateRequestMessage(method, "/api/persons");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetPerson_WhenPersonExist_ShouldReturn200StatusCode(string method)
        {
            // Arrange
            var request = CreateRequestMessage(method, "api/persons/13");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("GET")]
        public async Task GetPerson_WhenPersonWithSuchIdDoesNotExist_ShouldReturn404StatusCode(string method)
        {
            // Arrange
            var request = CreateRequestMessage(method, "api/persons/777");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("PUT")]
        public async Task UpdatePerson_WhenPersonWithSuchIdExists_ShouldReturn201StatusCode(string method)
        {
            // Arrange
            var personForCreation = new PersonDto
            {
                Name = "Test",
                Address = "Test Address",
                DateBirth = new DateTime(1993, 07, 27),
                Email = "test@gmail.com",
                Gender = GenderType.Male
            };
            var requestObj = new {id = 13, personForUpdate = personForCreation};

            var request = CreateRequestMessage(method, "api/persons/13", requestObj);

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Theory]
        [InlineData("PUT")]
        public async Task UpdatePerson_WhenPersonWithSuchIdDoesNotExists_ShouldReturn404StatusCode(string method)
        {
            // Arrange
            var personForCreation = new PersonDto
            {
                Name = "Test",
                Address = "Test Address",
                DateBirth = new DateTime(1993, 07, 27),
                Email = "test@gmail.com",
                Gender = GenderType.Male
            };
            var requestObj = new { id = 13, personForUpdate = personForCreation };

            var request = CreateRequestMessage(method, "api/persons/777", requestObj);

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        //[Theory]
        //[InlineData("PATCH")]
        //public async Task UpdatePersonAddress_WhenSuchPersonWithSuchIdExists_ShoudUpdateAddress(string method)
        //{
        //    // Arrange
        //    List<Operation<PersonDto>> operations = new List<Operation<PersonDto>>
        //    {
        //        new Operation<PersonDto>
        //        {
        //            op = "replace",
        //            path = "address",
        //            value = "updated address"
        //        }
        //    };
        //    var jsonPatchDocument = new JsonPatchDocument<PersonDto>(operations, new DefaultContractResolver());

        //    var requestObj = new {id = 13, personPatch = jsonPatchDocument};

        //    var request = CreateRequestMessage(method, "api/persons/13", requestObj);

        //    // Act
        //    var response = await Client.SendAsync(request);

        //    // Assert
        //    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        //}
    }
}
