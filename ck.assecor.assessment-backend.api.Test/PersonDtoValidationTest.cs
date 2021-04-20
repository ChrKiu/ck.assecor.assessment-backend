using ck.assecor.assessment_backend.api.Dtos;
using ck.assecor.assessment_backend.api.Test.TestData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Xunit;

namespace ck.assecor.assessment_backend.api.Test
{
    public class PersonDtoValidationTest
    {
        [Theory]
        [ClassData(typeof(MissingPersonPostDataTest))]
        public void CreatePerson_Gets_wrong_data_Returns_bad_request(string name, string lastName, string zipCode, string city, string color)
        {
            //arrange
            var personDto = new PersonDto();
            personDto.Name = name;
            personDto.LastName = lastName;
            personDto.ZipCode = zipCode;
            personDto.City = city;
            personDto.Color = color;

            //act

            var context = new ValidationContext(personDto);
            var results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(personDto, context, results, true);

            //assert
            valid.Should().BeFalse("because we only entered data with at least one missing required field");
        }
    }
}
