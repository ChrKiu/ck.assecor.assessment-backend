using ck.assecor.assessment_backend.api.Dtos;
using ck.assecor.assessment_backend.api.Extensions.Mappers;
using ck.assecor.assessment_backend.infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Web.Http.ModelBinding;

namespace ck.assecor.assessment_backend.api.Controllers
{

    [Route("persons")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;

        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }

        /// <summary>
        /// </summary>
        /// <returns>The <see cref="PersonDto"/> with the requested id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PersonDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ModelState), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreatePerson(PersonDto personDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var person = this.personService.CreatePerson(personDto.MapToPerson());
            var resultDto = person.MapToPersonDto();

            return Ok(resultDto);
        }

        /// <summary>
        /// </summary>
        /// <param name="id">The id of a person</param>
        /// <returns>The <see cref="PersonDto"/> with the requested id</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetPersonById(string id)
        {
            if(long.TryParse(id, out long parsedId))
            {
                return BadRequest();
            }

            var person = this.personService.GetPersonById(parsedId);
            var personDto = person.MapToPersonDto();
            
            return Ok(personDto);
        }

        /// <summary>
        /// </summary>
        /// <returns>All <see cref="PersonDto"/></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PersonDto[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetPersons()
        {
            var persons = this.personService.GetPersons();
            var personDtos = persons.Select(p => p.MapToPersonDto());

            return Ok(personDtos);
        }

        /// <summary>
        /// </summary>
        /// <param name="color">The favourite color</param>
        /// <returns>The <see cref="PersonDto"/> with the favourite color</returns>
        [HttpGet("color/{color}")]
        [ProducesResponseType(typeof(PersonDto[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetPersonsByColor(string color)
        {
            var persons = this.personService.GetPersonsBy(color);
            var personDtos = persons.Select(p => p.MapToPersonDto());

            return Ok(personDtos);
        }
    }
}
