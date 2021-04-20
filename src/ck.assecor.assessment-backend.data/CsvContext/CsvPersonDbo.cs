using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ck.assecor.assessment_backend.data.CsvContext
{
    public class CsvPersonDbo
    {
        [Ignore]
        public long Id { get; set; }

        [Index(1)]
        public string Name { get; set; }

        [Index(0)]
        public string LastName { get; set; }

        [Index(2)]
        public string ZipCodeAndCity
        {
            get { return $"{ZipCode} {City}"; }
            set 
            { 
                SetCityAndZipCode(value);
            }
        }

        [Ignore]
        public string City { get; set; }

        [Ignore]
        public string ZipCode { get; set; }

        [Index(3)]
        public string Color { get; set; }

        private void SetCityAndZipCode(string zipCodeAndCity)
        {
            var trimmedZipCodeAndCity = zipCodeAndCity.Trim();
            if(trimmedZipCodeAndCity == "")
            {
                return;
            }
            var splitValues = trimmedZipCodeAndCity.Split(" ",2);
            ZipCode = splitValues[0];
            City = splitValues[1];
        }

        public static bool TryToBuild(string lastname, string name, string zipAndCity, string color, long id, out CsvPersonDbo dbo)
        {
            dbo = new CsvPersonDbo();
            dbo.Id = id;
            dbo.LastName = lastname;
            dbo.Name = name;
            dbo.SetCityAndZipCode(zipAndCity);
            dbo.Color = color;

            if(dbo.LastName == string.Empty ||
                dbo.Name == string.Empty ||
                dbo.ZipCode == string.Empty ||
                dbo.City == string.Empty ||
                dbo.Color == string.Empty)
            {
                return false;
            }

            return true;
        }
    }
}
