using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Exceptions
{
    public class NameLengthException : Exception
    {
        public NameLengthException(string message) : base(message) { }
    }
}
