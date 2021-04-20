using ck.assecor.assessment_backend.infrastructure.Models.Persons;
using System.Collections.Generic;

namespace ck.assecor.assessment_backend.infrastructure.Interfaces
{
    /// <summary>
    /// Interface to access <see cref="Person"/> functionalities
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        ///     Creates a <see cref="Person"/>
        /// </summary>
        /// <param name="person">The person to be created</param>
        /// <returns>The created <see cref="Person"/></returns>
        public Person CreatePerson(Person person);

        /// <summary>
        ///     Gets a <see cref="Person"/> by it's ID
        /// </summary>
        /// <param name="id">The <paramref name="id"/> of the <see cref="Person"/></param>
        /// <returns>The <see cref="Person"/> with the requested <paramref name="id"/></returns>
        public Person GetPersonById(long id);

        /// <summary>
        ///     Returns all <see cref="Person"/>
        /// </summary>
        /// <returns>All <see cref="Person"/></returns>
        public IEnumerable<Person> GetPersons();

        /// <summary>
        ///     Returns <see cref="Person"/> by search parameters
        /// </summary>
        /// <param name="color">The favourite color of a person</param>
        /// <returns>All <see cref="Person"/> with the fitting search parameter</returns>
        public IEnumerable<Person> GetPersonsBy(string color = "");
    }
}
