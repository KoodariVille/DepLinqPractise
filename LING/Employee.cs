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
        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Department Department { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } 
        public DateTime? DateOfBirth { get; set; }
        public string Name => $"{FirstName} {LastName}";
        public int Age => (DateOfBirth == null) ? 0: (int)((DateTime.Now - DateOfBirth)?.TotalDays / 365.2421875);
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

        public Employee(int id, string first, string last, DateTime dob, double salary)
        {
            Id = id;
            FirstName = first;
            LastName = last;
            DateOfBirth = dob;
            Salary = salary;
            StartDate = DateTime.Now;
            EndDate = null;
        }

        public override string ToString() => $"{Id} {FirstName} {LastName}";
    }
}
