using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;

namespace Core.Models
{
    public class Picket : BaseModel
    {

        public string Name { get; protected set; }

        public int? IdProfile { get; protected set; }

        protected Picket (int id, string name, int? idProfile)
        {
            Id = id;
            Name = name;
            IdProfile = idProfile;
        }

        public static Picket Create(int id, string name, int? idProfile)
        {
            return new Picket(id, name, idProfile);
        }
    }
}
