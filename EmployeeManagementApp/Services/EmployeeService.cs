using EmployeeManagement.Exceptions;
using EmployeeManagement.Models;
using Newtonsoft.Json;
using System.Xml;

namespace EmployeeManagement.Services
{
    public static class EmployeeService
    {
        public static List<Employee> employees = new List<Employee>();
        private const string filePath = "employees.json";
        public static void SaveToFile()
        {
            var jsonData = JsonConvert.SerializeObject(employees, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }
        public static void LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            try
            {
                string jsonData = File.ReadAllText(filePath);
                employees = JsonConvert.DeserializeObject<List<Employee>>(jsonData) ?? new List<Employee>();
            }
            catch
            {
                throw new Exceptions.FileLoadException("File cannot be read");
            }
        }
        public static void AddEmployee(Employee newEmployee)
        {
            bool found = false;
            foreach (Employee emp in employees)
            {
                if (emp.ID == newEmployee.ID)
                {
                    found = true;
                    break;
                }
            }
            if (found == true)
            {
                throw new DuplicateEmployeeException($"This ID ({newEmployee.ID}) already exists");
            }
            employees.Add(newEmployee);
            SaveToFile();
        }
        public static void RemoveEmployee(int deleteEmployeeID)
        {
            Employee deleteEmployee = null;
            foreach (Employee emp in employees)
            {
                if (emp.ID == deleteEmployeeID)
                {
                    deleteEmployee = emp;
                    break;
                }
            }
            if (deleteEmployee == null)
            {
                throw new EmployeeNotFoundException($"Employee that will be deleted ({deleteEmployeeID}) wasn't found");
            }
            employees.Remove(deleteEmployee);
            SaveToFile();
        }
        public static void UpdateEmployee(int id, string newName, Position? newPosition, decimal? newSalary)
        {
            Employee foundedEmployee = null;
            foreach (Employee emp in employees)
            {
                if (emp.ID == id)
                {
                    foundedEmployee = emp;
                    break;
                }
            }
            if (foundedEmployee == null)
            {
                throw new EmployeeNotFoundException("Updatable employee couldn't found");
            }
            if (!string.IsNullOrEmpty(newName))
            {
                foundedEmployee.Name = newName;
            }
            if (newPosition != null)
            {
                foundedEmployee.Position = newPosition;
            }
            if (newSalary != null)
            {
                foundedEmployee.Salary = newSalary;
            }
            SaveToFile();
        }
        public static void ListEmployees()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("List is empty");
                return;
            }
            foreach (Employee emp in employees)
            {
                emp.PrintInfo();
            }
        }
        public static Employee SearchEmployee(int id)
        {
            if (employees.Any(s => s.ID == id))
            {
                Employee findedEmployee = employees.Find(s => s.ID == id);
                return findedEmployee;
            }
            else
            {
                Console.WriteLine("No such employee exists");
            }
            return null;
        }
        public static List<Employee> SortEmployees(string criteria)
        {
            //switch (criteria.Trim().ToLower())
            //{
            //    case "id":
            //        return employees.OrderBy(emp => emp.ID).ToList();
            //    case "name":
            //        return employees.OrderBy(emp => emp.Name).ToList();
            //    case "salary":
            //        return employees.OrderBy(emp => emp.Salary).ToList();
            //    case "date":
            //        return employees.OrderBy(emp => emp.hireDate).ToList();
            //    default:
            //        return employees;
            //}
            List<Employee> sorted = criteria.ToLower() switch
            {
                "id" => employees.OrderBy(emp => emp.ID).ToList(),
                "name" => employees.OrderBy(emp => emp.Name).ToList(),
                "salary" => employees.OrderBy(emp => emp.Salary).ToList(),
                "date" => employees.OrderBy(emp => emp.hireDate).ToList(),
                _ => employees.ToList(),
            };
            sorted.ForEach(emp => emp.PrintInfo());
            return sorted;
        }
        public static void FilterEmployees(decimal minSalary, decimal maxSalary)
        {
            List<Employee> matchedEmployees = new List<Employee>();
            foreach (Employee emp in employees)
            {
                decimal salary = emp.Salary.GetValueOrDefault(0);
                if (salary >= minSalary && salary <= maxSalary)
                {
                    matchedEmployees.Add(emp);
                }
            }
            if (matchedEmployees.Count == 0)
            {
                Console.WriteLine($"No employee in this salary interval");
            }
            else
            {
                Console.WriteLine($"Founded : {matchedEmployees.Count} employee(s)");
                foreach (Employee emp in matchedEmployees)
                {
                    emp.PrintInfo();
                }
            }
            //foreach (Employee emp in employees)
            //{
            //    decimal salary;
            //    if (emp.Salary.HasValue)
            //    {
            //        salary = emp.Salary.Value;
            //    }
            //    else
            //    {
            //        salary = 0;
            //    }
            //    if (salary >= minSalary && salary <= maxSalary)
            //    {
            //        matchedEmployees.Add(emp);
            //    }
            //}
        }
    }
}
