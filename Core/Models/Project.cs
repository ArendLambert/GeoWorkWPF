using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Project
    {
        public int Id { get; private set; }

        public string? Name { get; private set; }

        public int? IdCustomer { get; private set; }

        public int? IdEmployee { get; private set; }

        private Project(int id, string? name, int? idCustomer, int? idEmployee)
        {
            Id = id;
            Name = name;
            IdCustomer = idCustomer;
            IdEmployee = idEmployee;
        }

        public static Project Create(int id, string? name, int? idCustomer, int? idEmployee)
        {
            return new Project(id, name, idCustomer, idEmployee);
        }
    }
}
