using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement.Exceptions;

namespace EmployeeManagement.Models
{
    public struct Contact
    {
        public string OfficeNumber { get; set; }
        public int Floor { get; set; }
        public Contact(string officeNumber, int floor)
        {
            if (string.IsNullOrEmpty(officeNumber))
            {
                throw new InvalidWorkInfoException("Office number cannot be empty");
            }
            if (floor <= 0)
            {
                throw new InvalidWorkInfoException("Floor must be positive");
            }
            OfficeNumber = officeNumber;
            Floor = floor;
        }
    }
}
