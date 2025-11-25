using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Exceptions
{
    public class FileLoadException : Exception
    {
        public FileLoadException(string message) : base(message) { }
    }
}
