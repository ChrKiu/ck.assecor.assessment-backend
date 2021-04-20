using AutoMapper;
using ck.assecor.assessment_backend.data.CsvContext;
using ck.assecor.assessment_backend.infrastructure.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ck.assecor.assessment_backend.data.Repositories
{
    /// <summary>
    /// Repsitory of <see cref="Person"/> to access CSV-Data
    /// </summary>
    public class CsvPersonRepository : IPersonRepository
    {
        private readonly CsvPersonContext personContext;
        private readonly IMapper mapper;

        public CsvPersonRepository(CsvPersonContext personContext, IMapper mapper)
        {
            this.personContext = personContext ?? throw new ArgumentNullException(nameof(personContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        ///<inheritdoc/>
        public Person Create(Person person)
        {
            var mappedPersonDbo = this.mapper.Map<CsvPersonDbo>(person);
            personContext.CreatePerson(mappedPersonDbo);

            return person;
        }

        ///<inheritdoc/>
        public IEnumerable<Person> Get(Expression<Func<Person, bool>> expression)
        {
            var mappedExpression = this.mapper.Map<Expression<Func<CsvPersonDbo, bool>>>(expression);

            Func<CsvPersonDbo, bool> predicate = mappedExpression.Compile();

            IEnumerable<CsvPersonDbo> result = personContext.GetPerson(predicate);

            return this.mapper.Map<IEnumerable<Person>>(result);
        }
    }
}
