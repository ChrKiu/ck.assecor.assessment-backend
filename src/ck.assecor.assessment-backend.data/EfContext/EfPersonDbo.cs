namespace ck.assecor.assessment_backend.data.EfContext
{
    public class EfPersonDbo 
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public int Color { get; set; }

        public static EfPersonDbo GetNullValue()
        {
            return new EfPersonDbo()
            {
                Id = 0,
                Name = "",
                LastName = "",
                City = "",
                ZipCode = "",
                Color = 0
            };
        }
    }
}
