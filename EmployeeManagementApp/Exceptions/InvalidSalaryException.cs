using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Exceptions
{
    public class InvalidSalaryException : Exception
    {
        public InvalidSalaryException(string message) : base(message) { }
    }
}
