using System.Collections;
using System.Collections.Generic;

namespace ck.assecor.assessment_backend.api.Test.TestData
{
    public class MissingPersonPostDataTest : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>();

        public MissingPersonPostDataTest()
        {
            CreateData();
        }

        public void CreateData()
        {
            var nameParameter = new string[] { "Name", null };
            var lastNameParameter = new string[] { "LastName", null };
            var zipCodeParameter = new string[] { "ZipCode", null };
            var cityParameter = new string[] { "City", null };
            var colorParameter = new string[] { "Blau", null };

            foreach (var name in nameParameter)
            {
                foreach (var lastName in lastNameParameter)
                {
                    foreach (var zipCode in zipCodeParameter)
                    {
                        foreach (var city in cityParameter)
                        {
                            foreach (var color in colorParameter)
                            {
                                if (name != null && lastName != null && zipCode != null && city != null && color != null)
                                {
                                    continue;
                                }
                                _data.Add( new object[] { name, lastName, zipCode, city, color });
                            }
                        }
                    }
                }
            }
        }

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
