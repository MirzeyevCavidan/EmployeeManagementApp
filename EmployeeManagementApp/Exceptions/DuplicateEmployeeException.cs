using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement
{
    public class DuplicateEmployeeException : Exception
    {
        public DuplicateEmployeeException(string message) : base(message) { }
    }
}
