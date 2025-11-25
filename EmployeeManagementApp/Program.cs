using System;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using EmployeeManagement.Exceptions;
using EmployeeManagement;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            EmployeeService.LoadFromFile();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n< Employee Management System >");
            Console.WriteLine("1 - Add Employee");
            Console.WriteLine("2 - Remove Employee");
            Console.WriteLine("3 - Search Employee");
            Console.WriteLine("4 - Update Employee");
            Console.WriteLine("5 - Sort Employees");
            Console.WriteLine("6 - List Employees");
            Console.WriteLine("7 - Filter Employees");
            Console.WriteLine("8 - Exit");
            Console.ResetColor();
            Console.WriteLine("Enter your choice: ");
            string choose = Console.ReadLine();
            switch (choose)
            {
                case "1":
                    AddEmployeeFlow();
                    break;
                case "2":
                    RemoveEmployeeFlow();
                    break;
                case "3":
                    SearchEmployeeFlow();
                    break;
                case "4":
                    UpdateEmployeeFlow();
                    break;
                case "5":
                    SortEmployeesFlow();
                    break;
                case "6":
                    ListEmployeesFlow();
                    break;
                case "7":
                    FilterEmployeesFlow();
                    break;
                case "8":
                    Console.WriteLine("Program is closing...");
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Choice. Please try again.");
                    Console.ResetColor();
                    break;
            }
        }
    }
    static void AddEmployeeFlow()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n< Add New Employee >");
        Console.ResetColor();
        try
        {
            Console.WriteLine("ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                throw new FormatException("ID must be a valid number.");
            }
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Hire Date (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime hireDate))
            {
                throw new FormatException("Invalid Date format.");
            }
            Employee newEmployee = new Employee(id, name, hireDate);
            Console.WriteLine("Department (5-6 chars): ");
            newEmployee.Department = Console.ReadLine();

            Console.WriteLine("Position (Junior, Middle, Senior, Manager): ");
            string posInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(posInput))
            {
                if (Enum.TryParse(typeof(Position), posInput, true, out object result))
                {
                    newEmployee.Position = (Position)result;
                }
                else
                {
                    Console.WriteLine("Invalid Position entered. Setting to null.");
                }
            }
            Console.WriteLine("Salary: ");
            string salInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(salInput) && decimal.TryParse(salInput, out decimal salary))
            {
                newEmployee.Salary = salary;
            }
            EmployeeService.AddEmployee(newEmployee);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Employee added successfully!");
            Console.ResetColor();
        }
        catch (DuplicateEmployeeException ex)
        {
            PrintErrorMessage(ex.Message);
        }
        catch (NameLengthException ex)
        {
            PrintErrorMessage(ex.Message);
        }
        catch (NameEmptyException ex)
        {
            PrintErrorMessage(ex.Message);
        }
        catch (InvalidSalaryException ex)
        {
            PrintErrorMessage(ex.Message);
        }
        catch (FormatException ex)
        {
            PrintErrorMessage(ex.Message);
        }
        catch (Exception ex)
        {
            PrintErrorMessage($"Unexpected error: {ex.Message}");
        }
    }
    static void RemoveEmployeeFlow()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n< Remove Employee >");
        Console.ResetColor();
        Console.WriteLine("Enter ID to remove: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            try
            {
                EmployeeService.RemoveEmployee(id);
                Console.WriteLine("Employee removed successfully.");
            }
            catch (EmployeeNotFoundException ex)
            {
                PrintErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                PrintErrorMessage(ex.Message);
            }
        }
        else
        {
            PrintErrorMessage("Invalid ID format.");
        }
    }
    static void SearchEmployeeFlow()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n< Search Employee >");
        Console.ResetColor();

        Console.WriteLine("Enter ID to search: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Employee emp = EmployeeService.SearchEmployee(id);

            if (emp != null)
            {
                Console.WriteLine("Employee Found:");
                emp.PrintInfo();
            }
        }
        else
        {
            PrintErrorMessage("Invalid ID format.");
        }
    }
    static void UpdateEmployeeFlow()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n< Update Employee >");
        Console.ResetColor();

        Console.WriteLine("Enter Employee ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            PrintErrorMessage("Invalid ID format.");
            return;
        }
        Employee existing = EmployeeService.SearchEmployee(id);
        if (existing == null) return;

        bool updating = true;
        while (updating)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nUpdating Employee ID: {id}");
            Console.WriteLine("1 - Update Name");
            Console.WriteLine("2 - Update Position");
            Console.WriteLine("3 - Update Salary");
            Console.WriteLine("4 - None/Go Back");
            Console.ResetColor();
            Console.WriteLine("Select property: ");
            string choice = Console.ReadLine();
            try
            {
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter New Name: ");
                        string newName = Console.ReadLine();
                        EmployeeService.UpdateEmployee(id, newName, null, null);
                        Console.WriteLine("Name updated.");
                        break;
                    case "2":
                        Console.WriteLine("Enter New Position (Junior, Middle, Senior, Manager): ");
                        string posStr = Console.ReadLine();
                        if (Enum.TryParse(typeof(Position), posStr, true, out object posResult))
                        {
                            EmployeeService.UpdateEmployee(id, null, (Position)posResult, null);
                            Console.WriteLine("Position updated.");
                        }
                        else
                        {
                            PrintErrorMessage("Invalid Position.");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Enter New Salary: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newSalary))
                        {
                            EmployeeService.UpdateEmployee(id, null, null, newSalary);
                            Console.WriteLine("Salary updated.");
                        }
                        else
                        {
                            PrintErrorMessage("Invalid Salary.");
                        }
                        break;
                    case "4":
                        updating = false;
                        break;
                    default:
                        PrintErrorMessage("Invalid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                PrintErrorMessage(ex.Message);
            }
        }
    }
    static void SortEmployeesFlow()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n< Sort Employees >");
        Console.ResetColor();
        Console.WriteLine("Criteria options: id, name, salary, date");
        Console.WriteLine("Enter criteria (or leave empty to list original): ");
        string criteria = Console.ReadLine();
        EmployeeService.SortEmployees(criteria);
    }
    static void ListEmployeesFlow()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n< List All Employees >");
        Console.ResetColor();
        EmployeeService.ListEmployees();
    }
    static void FilterEmployeesFlow()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n< Filter by Salary >");
        Console.ResetColor();
        try
        {
            Console.WriteLine("Min Salary: ");
            decimal min = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Max Salary: ");
            decimal max = decimal.Parse(Console.ReadLine());

            EmployeeService.FilterEmployees(min, max);
        }
        catch (FormatException)
        {
            PrintErrorMessage("Please enter valid numbers for salary.");
        }
    }
    static void PrintErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {message}");
        Console.ResetColor();
    }
}