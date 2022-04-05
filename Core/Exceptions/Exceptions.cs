using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class ParameterNotFoundException : Exception
    {
        public ParameterNotFoundException(string message) : base(message)
        {

        }
    }

    public class RecordNoFoundException : Exception
    {
        public RecordNoFoundException(string message) : base(message)
        {

        }
    }

    public class SqlServerException : Exception
    {
        public SqlServerException(string message) : base(message)
        {

        }
    }
}
