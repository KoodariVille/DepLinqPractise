using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LING
{
    public static class Application
    {
        public static List<MenuItem> Menu;

        public static void WriteResult<T>(int itemid, List<T> result) {
            string row;
            //otsikkorivit
            WriteLine();
            WriteLine(Menu.Where(mi => mi.Id == itemid).First().Name.ToUpper());
            WriteLine("‐".PadRight(18 * result[0].GetType().GetProperties().Length + 2, '‐'));
            //sarakeotsikkorivit
            if (result.Count > 0)
            {
                row = "";
                foreach (PropertyInfo property in result[0].GetType().GetProperties())
                {
                    row += $"{property.Name}".PadRight(16) + " | ";
                }
                WriteLine(row);
            }
            WriteLine("‐".PadRight(18 * result[0].GetType().GetProperties().Length + 2, '‐'));
            //datarivit
            foreach (object item in result)
            {
                row = "";
                foreach (PropertyInfo property in item.GetType().GetProperties())
                {
                    row += $"{property.GetValue(item, null).ToString()}".PadRight(16) + " | ";
                }
                WriteLine(row);
            }
            WriteLine("‐".PadRight(18 * result[0].GetType().GetProperties().Length + 2, '‐'));
            WriteLine();
            Write("Paina Enter jatkaaksesi.");
            ReadLine();
        }

        public static void InitializeMenu()
        {
            Menu = new List<MenuItem> {
            new MenuItem() { Id = 1, Name = "50-vuotiaat työntekijä" },
            new MenuItem() { Id = 2, Name = "Osastot yli 50 henkilöä"},
            new MenuItem() { Id = 3, Name = "Sukunimen työntekijät"},
            new MenuItem() { Id = 4, Name = "Osastojen isoimmat palkat"},
            new MenuItem() { Id = 5, Name = "Viisi yleisintä sukunimeä"},
            new MenuItem() { Id = 6, Name = "Osastojen ikäjakaumat"}                
            };

            Menu[0].ItemSelected += (obj, a) =>
            {
                var result = Data.Employees
                .Where(e => e.Age == 50)
                .Select(e => new { Nimi = e.Name, Ikä = e.Age, Osasto = e.Department })
                .ToList();
                WriteResult(a.ItemId, result);
            };

            Menu[1].ItemSelected += (obj, a) =>
            {
                var result = Data.Departments
                .Where(e => e.EmployeeCount > 50)
                .Select(e => new { Id = e.Id, Nimi = e.Name, Vahvuus = e.EmployeeCount })
                .OrderByDescending(e => e.Vahvuus)
                .ToList();
                WriteResult(a.ItemId, result);
            };

            Menu[2].ItemSelected += (obj, a) =>
            {
                string snimi = ReadLine();
                var result = Data.Employees
                .Where(e => snimi == e.LastName)
                .Select(e => new { Id = e.Id, Nimi = e.Name })
                .ToList();
                WriteResult(a.ItemId, result);
            };

            Menu[3].ItemSelected += (obj, a) =>
            {
                var result = Data.Departments.SelectMany(d => d.Employees,
                    (d, e) => new { Osasto = d.Name, Palkka = e.Salary })
                    .GroupBy(e => e.Osasto)
                    .Select(e => new { Osasto = e.Key, Maksimipalkka = e.Max() })
                    .ToList();
                WriteResult(a.ItemId, result);
            };

            Menu[4].ItemSelected += (obj, a) =>
            {
                var result = Data.Employees
                .GroupBy(e => e.LastName)
                .Select(e => new { SukuNimi = e.Key, Lkm = e.Count() })
                .OrderByDescending(x => x.Lkm)
                .Take(5)
                .ToList();
                WriteResult(a.ItemId, result);
            };

            Menu[5].ItemSelected += (obj, a) =>
            {
                var result = Data.Departments
                .Select(e => new { Nimi = e.Name, Alle30v = e.Employees.Count})
                .ToList();
                WriteResult(a.ItemId, result);
            };
        }

        public static void Initialize()
        {
            Data.GenerateData();
            InitializeMenu();         
        }
        
        public static int ReadFromMenu()
        {
            System.Console.Clear();
            WriteLine("Vaihtoehdot");
            foreach(MenuItem e in Menu)
            {
                WriteLine(e);
            }
            WriteLine("Valitse (0=lopetus):");
            int k = int.Parse(ReadLine());
            if(k < 0 || k > 6)
            {
                throw new Exception("Vastaus ei ole kelvollinen");
            }
            else
            {
                return k;
            }         
        }

        public static void Run()
        {
            Initialize();
            while(ReadFromMenu() != 0)
            {
                int i = ReadFromMenu();
                Menu[i].Select();
            }
        }
    }
}
