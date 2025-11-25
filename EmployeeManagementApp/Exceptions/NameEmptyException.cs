using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Exceptions
{
    public class NameEmptyException : Exception
    {
        public NameEmptyException(string message) : base(message) { }
    }
}
