using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LING
{
    public class Employee
    {
        private double _salary;
        int Id { get; }
        string FirstName { get; set; }
        string LastName { get; set; }
        Department Department { get; set; }
        DateTime StartDate { get; set; }
        DateTime? EndDate { get; set; } 
        DateTime? DateOfBirth { get; set; }
        string Name => $"{FirstName} {LastName}";
        int Age => (DateOfBirth == null) ? 0: (int)((DateTime.Now - DateOfBirth)?.TotalDays / 365.2421875);
        public Double Salary{
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        _salary = value;
                    }
                }
                catch
                {
                    throw new Exception("Salary can´t be negative.");
                }
            }
            get => Math.Round(_salary, 2);
        }
    }
}
