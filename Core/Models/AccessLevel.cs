using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;

namespace Core.Models
{
    public class AccessLevel : BaseModel
    {
        public string Name { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        private AccessLevel(int id, string name, string description)
        {
            Id = id; Name = name; Description = description;
        }

        public static AccessLevel Create(int id, string name, string description)
        {
            return new AccessLevel(id, name, description);
        }
    }
}
