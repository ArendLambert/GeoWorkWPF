using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;

namespace Core.Models
{
    public class Project : BaseModel
    {

        public string Name { get; protected set; }

        public int? IdCustomer { get; protected set; }

        public int? IdEmployee { get; protected set; }

        protected Project(int id, string name, int? idCustomer, int? idEmployee)
        {
            Id = id;
            Name = name;
            IdCustomer = idCustomer;
            IdEmployee = idEmployee;
        }

        public static Project Create(int id, string name, int? idCustomer, int? idEmployee)
        {
            return new Project(id, name, idCustomer, idEmployee);
        }
    }
}
