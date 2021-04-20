using System.ComponentModel.DataAnnotations;

namespace ck.assecor.assessment_backend.api.Dtos
{

    /// <summary>
    /// The dto for a person which is given out and received from the api interface
    /// </summary>
    public class PersonDto
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Color { get; set; }
    }
}
