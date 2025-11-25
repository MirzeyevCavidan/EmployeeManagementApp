using EmployeeManagement.Exceptions;
using System;
using System.Xml.Linq;

namespace EmployeeManagement.Models
{
    public class Employee : Person, IPrintable
    {
        public Position? Position { get; set; }

        private decimal? salary;
        public decimal? Salary
        {
            get { return salary; }
            set
            {
                if (value <= 0)
                {
                    throw new InvalidSalaryException("Salary must be positive");
                }
                salary = value;
            }
        }
        private string _department;
        public string Department
        {
            get { return _department; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new NameEmptyException("Department cannot be empty");
                }
                if (value.Length < 5 || value.Length > 6)
                {
                    throw new NameLengthException("Department name length must be 5 or 6");
                }
                _department = value;
            }
        }
        public DateTime hireDate { get; private set; }
        public Employee(int id, string name, DateTime HireDate)
        {
            ID = id;
            Name = name;
            hireDate = HireDate;
        }
        public override void PrintInfo()
        {
            string positionString = Position?.ToString() ?? "N/A";
            string salaryString = Salary?.ToString() ?? "N/A";
            string deptString = Department ?? "N/A";
            Console.WriteLine
            (
                $"ID: {ID}\n" +
                $"Name: {Name}\n" +
                $"Department: {deptString}\n" +
                $"Position: {positionString}\n" +
                $"Salary: {salaryString}\n" +
                $"Hire Date: {hireDate}\n"
            );
        }
    }
}