using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ck.assecor.assessment_backend.infrastructure.Models.Persons
{
    /// <summary>
    /// Repository for creating and getting <see cref="Person"/>
    /// </summary>
    public interface IPersonRepository
    {
        Person Create(Person person);
        IEnumerable<Person> Get(Expression<Func<Person, bool>> expression);
    }
}
