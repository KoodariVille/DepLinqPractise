using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LING
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
        public int EmployeeCount => Employees.Count;

        public Department()
        {
            Employees = new List<Employee>();
        }

        public Department(int id, string name) : this() {
            Id = id;
            Name = name;
        }

        public override string ToString() => $"{Name} ({EmployeeCount})";
    }
}
