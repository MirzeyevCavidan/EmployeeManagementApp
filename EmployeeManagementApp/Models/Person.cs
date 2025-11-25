using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement.Exceptions;

namespace EmployeeManagement.Models
{
    public abstract class Person
    {
        public int ID { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new NameEmptyException("Name cannot be empty");
                }
                if (value.Length < 3 || value.Length > 25)
                {
                    throw new NameLengthException("Name length must be between 3 and 25");
                }
                name = value;
            }
        }
        public abstract void PrintInfo();
    }
}
