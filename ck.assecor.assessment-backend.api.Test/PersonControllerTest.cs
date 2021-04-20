using ck.assecor.assessment_backend.api.Controllers;
using ck.assecor.assessment_backend.api.Dtos;
using ck.assecor.assessment_backend.api.Extensions.Mappers;
using ck.assecor.assessment_backend.infrastructure.Interfaces;
using ck.assecor.assessment_backend.infrastructure.Models.Persons;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace ck.assecor.assessment_backend.api.Test

{ 
    public class PersonControllerTest
    {


        [Theory]
        [InlineData("Name", "LastName", "ZipCode", "City", "blau")]
        public void CreatePerson_Gets_rightData_Returns_ok_with_created_person(string name, string lastName, string zipCode, string city, string color)
        {
            //arrange
            var personService = Substitute.For<IPersonService>();
            
            var personController = new PersonController(personService);

            var personDto = new PersonDto();
            personDto.Name = name;
            personDto.LastName = lastName;
            personDto.ZipCode = zipCode;
            personDto.City = city;
            personDto.Color = color;

            var person = personDto.MapToPerson();

            var returnPersonDto = new PersonDto();
            returnPersonDto.Id = 1;
            returnPersonDto.Name = name;
            returnPersonDto.LastName = lastName;
            returnPersonDto.ZipCode = zipCode;
            returnPersonDto.City = city;
            returnPersonDto.Color = color;

            var returnPerson = returnPersonDto.MapToPerson();

            personService.CreatePerson(default).ReturnsForAnyArgs(returnPerson);
            //act
            var result = personController.CreatePerson(personDto);
            
            //assert
            personService.Received().CreatePerson(Arg.Any<Person>());

            var objectResult = result as OkObjectResult;
            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200, "because the controller should work with this dataset");

            var model = objectResult.Value as PersonDto;
            model.Should().NotBeNull();
            model.Should().BeEquivalentTo(returnPersonDto, "because we expect the controller to return the mapped value which the service returns");
        }

        [Fact]
        public void GetPersons_Gets_called_Returns_ok_with_all_persons()
        {
            //arrange
            var personService = Substitute.For<IPersonService>();

            var personController = new PersonController(personService);

            var personDto1 = new PersonDto();
            personDto1.Id = 1;
            personDto1.Name = "name";
            personDto1.LastName = "lastName";
            personDto1.ZipCode = "zipCode";
            personDto1.City = "city";
            personDto1.Color = "blau";

            var person1 = personDto1.MapToPerson();

            var personDto2 = new PersonDto();
            personDto2.Id = 2;
            personDto2.Name = "name2";
            personDto2.LastName = "lastName2";
            personDto2.ZipCode = "zipCode2";
            personDto2.City = "city2";
            personDto2.Color = "blau";

            var person2 = personDto2.MapToPerson();

            var returnValue = new List<Person>();
            returnValue.Add(person1);
            returnValue.Add(person2);

            personService.GetPersons().ReturnsForAnyArgs(returnValue);

            //act
            var result = personController.GetPersons();

            //assert
            personService.Received(1).GetPersons();

            var objectResult = result as OkObjectResult;
            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200, "because the controller should work with this dataset");

            var model = objectResult.Value as IEnumerable<PersonDto>;
            model.Should().NotBeNull();
            model.Should().ContainEquivalentOf(personDto1, "because we expect the controller to return the mapped value which the service returns");
            model.Should().ContainEquivalentOf(personDto2, "because we expect the controller to return the mapped value which the service returns");
        }

        [Fact]
        public void GetPersonByColor_Gets_called_Returns_ok_with_all_persons()
        {
            //arrange
            var personService = Substitute.For<IPersonService>();

            var personController = new PersonController(personService);

            var personDto1 = new PersonDto();
            personDto1.Id = 1;
            personDto1.Name = "name";
            personDto1.LastName = "lastName";
            personDto1.ZipCode = "zipCode";
            personDto1.City = "city";
            personDto1.Color = "blau";

            var person1 = personDto1.MapToPerson();

            var personDto2 = new PersonDto();
            personDto2.Id = 2;
            personDto2.Name = "name2";
            personDto2.LastName = "lastName2";
            personDto2.ZipCode = "zipCode2";
            personDto2.City = "city2";
            personDto2.Color = "blau";

            var person2 = personDto2.MapToPerson();

            var returnValue = new List<Person>();
            returnValue.Add(person1);
            returnValue.Add(person2);

            personService.GetPersonsBy(Arg.Any<string>()).ReturnsForAnyArgs(returnValue);

            //act
            var result = personController.GetPersonsByColor("color");

            //assert
            personService.Received(1).GetPersonsBy(Arg.Any<string>());

            var objectResult = result as OkObjectResult;
            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(200, "because the controller should work with this dataset");

            var model = objectResult.Value as IEnumerable<PersonDto>;
            model.Should().NotBeNull();
            model.Should().ContainEquivalentOf(personDto1, "because we expect the controller to return the mapped value which the service returns");
            model.Should().ContainEquivalentOf(personDto2, "because we expect the controller to return the mapped value which the service returns");
        }
    }
}
