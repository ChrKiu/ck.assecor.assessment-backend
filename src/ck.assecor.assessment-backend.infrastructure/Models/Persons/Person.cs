using ck.assecor.assessment_backend.infrastructure.Models.Base;

namespace ck.assecor.assessment_backend.infrastructure.Models.Persons
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public Color Color { get; set; }
    }
}
