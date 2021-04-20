using ck.assecor.assessment_backend.api.Dtos;
using ck.assecor.assessment_backend.infrastructure.Models;
using ck.assecor.assessment_backend.infrastructure.Models.Persons;
using System;

namespace ck.assecor.assessment_backend.api.Extensions.Mappers
{
    /// <summary>
    /// Extensionmethods for mapping to and from <see cref="PersonDto"/> and <see cref="Person"/>
    /// </summary>
    public static class PersonDtoMapper
    {
        public static PersonDto MapToPersonDto(this Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                LastName = person.LastName,
                ZipCode = person.ZipCode,
                City = person.City,
                Color = person.Color.ToString()
            };
        }

        public static Person MapToPerson(this PersonDto personDto)
        {
            if(!Enum.TryParse(personDto.Color, out Color parsedColor) )
            {
                parsedColor = Color.undefined;
            }

            return new Person
            {
                Id = personDto.Id,
                Name = personDto.Name,
                LastName = personDto.LastName,
                ZipCode = personDto.ZipCode,
                City = personDto.City,
                Color = parsedColor
            };
        }
    }
}
