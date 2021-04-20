using System;

namespace ck.assecor.assessment_backend.infrastructure.Exceptions
{
    /// <summary>
    /// Exception which occurs when there are invalid search parameters
    /// </summary>
    public class InvalidSearchParameterException : Exception
    {
        public InvalidSearchParameterException(string? msg) :base(msg)
        {

        }

    }
}
