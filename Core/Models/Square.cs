using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Square
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public int Alitude { get; private set; }

        public int? IdProject { get; private set; }

        private Square(int id, string name, int alitude, int? idProject)
        {
            Id = id;
            Name = name;
            Alitude = alitude;
            IdProject = idProject;
        }

        public static Square Create(int id, string name, int alitude, int? idProject)
        {
            return new Square(id, name, alitude, idProject);
        }
    }
}
