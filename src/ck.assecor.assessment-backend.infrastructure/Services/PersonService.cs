using ck.assecor.assessment_backend.infrastructure.Exceptions;
using ck.assecor.assessment_backend.infrastructure.Interfaces;
using ck.assecor.assessment_backend.infrastructure.Models;
using ck.assecor.assessment_backend.infrastructure.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ck.assecor.assessment_backend.infrastructure.Services
{
    ///<inheritdoc/>
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personRepo;

        ///<inheritdoc/>
        public PersonService(IPersonRepository personRepo)
        {
            this.personRepo = personRepo ?? throw new ArgumentNullException(nameof(personRepo));
        }

        ///<inheritdoc/>
        public Person GetPersonById(long id)
        {
            return personRepo.Get(p => p.Id == id).FirstOrDefault();
        }

        ///<inheritdoc/>
        public IEnumerable<Person> GetPersons()
        {
            return personRepo.Get(p => true);
        }

        ///<inheritdoc/>
        public IEnumerable<Person> GetPersonsBy(string color = "")
        {
            if (Enum.TryParse(color, out Color parsedColor))
            {
                if (parsedColor != Color.undefined)
                {
                    return personRepo.Get(p => p.Color == parsedColor);
                }
            }

            throw new InvalidSearchParameterException("No valid parameter to search for persons.");
        }

        ///<inheritdoc/>
        public Person CreatePerson(Person person)
        {
            return personRepo.Create(person);
        }
    }
}
