using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Customer
    {
        public int Id { get; private set; }

        public int? IdType { get; private set; }

        private Customer(int id, int? idType)
        {
            Id = id; IdType = idType;
        }

        public static Customer Create(int id, int? idType)
        {
            return new Customer(id, idType);
        }
    }
}
