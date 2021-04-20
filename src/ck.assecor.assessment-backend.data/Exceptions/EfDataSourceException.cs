using System;

namespace ck.assecor.assessment_backend.data.Exceptions
{
    /// <summary>
    /// Exception which occurs when there is an Error while accessing the EF-Data-Source
    /// </summary>
    public class EfDataSourceException :Exception
    {
        public EfDataSourceException(string? msg) :base(msg)
        {

        }
    }
}
