using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;

namespace Core.Models
{
    public class CustomerType : BaseModel
    {
        public int Id { get; private set; }

        public string Description { get; private set; } = string.Empty;

        private CustomerType (int id, string description) 
        {  
            Id = id; Description = description; 
        }

        public static CustomerType Create(int id, string description)
        {
            return new CustomerType(id, description);
        }
    }
}
