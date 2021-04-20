using System;

namespace ck.assecor.assessment_backend.data.Exceptions
{
    /// <summary>
    /// Exception which occurs when there is an Error while accessing the CSV-Data-Source
    /// </summary>
    public class CsvDataSourceException : Exception
    {
        public CsvDataSourceException(string? msg) : base(msg)
        {

        }
    }
}
