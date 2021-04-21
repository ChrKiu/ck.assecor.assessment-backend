using AutoMapper;
using ck.assecor.assessment_backend.data.EfContext;
using ck.assecor.assessment_backend.data.Exceptions;
using ck.assecor.assessment_backend.infrastructure.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ck.assecor.assessment_backend.data.Repositories
{
    /// <summary>
    /// Repository for <see cref="Person"/> to access a DB over Entityframe
    /// </summary>
    public class EfPersonRepository : IPersonRepository
    {
        private readonly EfDbContext context;
        private readonly IMapper mapper;

        public EfPersonRepository(EfDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        ///<inheritdoc/>
        public Person Create(Person person)
        {
            try
            {
                var mappedPersonDbo = this.mapper.Map<EfPersonDbo>(person);
                context.Persons.Add(mappedPersonDbo);
                context.SaveChanges();

                return this.mapper.Map<Person>(mappedPersonDbo);
            }
            catch (Exception e)
            {
                throw new EfDataSourceException(e.Message);
            }
            
        }

        ///<inheritdoc/>
        public IEnumerable<Person> Get(Expression<Func<Person, bool>> expression)
        {
            try
            {
                var mappedExpression = this.mapper.Map<Expression<Func<EfPersonDbo, bool>>>(expression);

                var personDbos = context.Persons.Where(mappedExpression);

                if(!personDbos.Any())
                {
                    personDbos.ToList().Add(EfPersonDbo.GetNullValue());
                }

                return this.mapper.Map<IEnumerable<Person>>(personDbos);
            }
            catch (Exception e)
            {
                throw new EfDataSourceException(e.Message);
            }
        }
    }
}
