using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Position
    {
        public int Id { get; private set; }

        public string? Name { get; private set; }

        public int? Salary { get; private set; }

        public int? IdAccessLevel { get; private set; }

        private Position(int id, string? name, int? salary, int? idAccessLevel)
        {
            Id = id;
            Name = name;
            Salary = salary;
            IdAccessLevel = idAccessLevel;
        }

        public static Position Create(int id, string? name, int? salary, int? idAccessLevel)
        {
            return new Position(id, name, salary, idAccessLevel);
        }
    }
}
