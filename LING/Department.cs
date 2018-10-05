using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LING
{
    public class Department
    {
        int Id { get; set; }
        string Name { get; set; }
        List<Employee> Employees { get; set; }
        int EmployeeCount => Employees.Count;

        Department()
        {
            Employees = new List<Employee>();
        }

        Department(int id, string name) : this() {
            Id = id;
            Name = name;
        }

        public override string ToString() => $"{Name} ({EmployeeCount})";
    }
}
