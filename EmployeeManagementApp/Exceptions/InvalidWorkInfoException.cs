using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Exceptions
{
    public class InvalidWorkInfoException : Exception
    {
        public InvalidWorkInfoException(string message) : base(message) { }
    }
}
