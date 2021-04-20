using ck.assecor.assessment_backend.data.Exceptions;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ck.assecor.assessment_backend.data.CsvContext
{
    /// <summary>
    /// Context for accessing the CSV-Data-Source of a <see cref="CsvPersonDbo"/>
    /// </summary>
    public class CsvPersonContext
    {
        private readonly ICsvConfiguration configuration;

        public CsvPersonContext(ICsvConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        ///     Creates a <see cref="CsvPersonDbo"/>
        /// </summary>
        /// <param name="personDbo"></param>
        public void CreatePerson(CsvPersonDbo personDbo)
        {
            try
            {
                using (var writer = new StreamWriter(this.configuration.Path, append: true))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.Writer.NextRecord();
                    csv.WriteRecord(personDbo);
                }

            }
            catch (Exception e)
            {
                throw new CsvDataSourceException(e.Message);
            }
        }

        /// <summary>
        ///     Returns a <see cref="CsvPersonDbo"/>
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<CsvPersonDbo> GetPerson(Func<CsvPersonDbo, bool> predicate)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                MissingFieldFound = MissingFieldHandler,
                TrimOptions = TrimOptions.Trim
            };
            try
            {
                var results = new List<CsvPersonDbo>();
                using (var reader = new StreamReader(this.configuration.Path))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<CsvPersonDbo>();

                    foreach (var r in records)
                    {
                        if(SkipCurrentIndex)
                        {
                            SkipCurrentIndex = false;
                            continue;
                        }
                        if (predicate.Invoke(r))
                        {
                            r.Id = csv.Context.Parser.RawRow;
                            results.Add(r);
                        }
                    }
                }

                var restoredRecords = RestoreBadRecords();
                var filteredRestoredRecords = restoredRecords.Where(p => predicate(p)).ToList();
                results.AddRange(filteredRestoredRecords);
                return results;

            }
            catch (Exception e)
            {
                throw new CsvDataSourceException(e.Message);
            }
        }

        private List<(long id, string rawRecord)> badRecords = new List<(long id, string rawRecord)>();

        private bool SkipCurrentIndex = false;
               
        /// <summary>
        /// Handler which gets executed when a field is missing
        /// </summary>
        /// <param name="args"></param>
        private void MissingFieldHandler(MissingFieldFoundArgs args)
        {
            SkipCurrentIndex = true;

            var index = args.Context.Parser.RawRow;
            var rawRecord = args.Context.Parser.RawRecord;
                     
            foreach (var badRecord in badRecords)
            {
                if(badRecord.id == index)
                {
                    return;
                }
            }

            badRecords.Add((index, rawRecord));
        }

        /// <summary>
        /// Function trying to restore records which have missing fields
        /// </summary>
        /// <returns></returns>
        private IEnumerable<CsvPersonDbo> RestoreBadRecords()
        {
            try
            {
                var resultList = new List<CsvPersonDbo>();

                for (int i = 0; i < badRecords.Count -1; i++)
                {
                    var combinedRecord = $"{badRecords[i].rawRecord}{badRecords[i + 1].rawRecord}";

                    var splittedRecord = combinedRecord.Split(",");

                    if (splittedRecord.Length == 4)
                    {
                        if (CsvPersonDbo.TryToBuild(splittedRecord[0], splittedRecord[1], splittedRecord[2], splittedRecord[3], badRecords[i].id, out CsvPersonDbo dbo))
                        {
                            resultList.Add(dbo);

                            //since we added two bad records together already, we don't need to use it again
                            i++;
                        }
                    }
                }
                
                return resultList;
            }
            finally
            {
                badRecords = new List<(long id, string rawRecord)>();
            }
        }
    }
}
