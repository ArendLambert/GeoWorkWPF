using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Profile
    {
        public int Id { get; private set; }

        public string? Name { get; private set; }

        public int? IdSquare { get; private set; }

        private Profile(int id, string? name, int? idSquare)
        {
            Id = id;
            Name = name;
            IdSquare = idSquare;
        }

        public static Profile Create(int id, string? name, int? idSquare)
        {
            return new Profile(id, name, idSquare);
        }
    }
}
