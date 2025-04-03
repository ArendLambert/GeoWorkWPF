using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;

namespace Core.Models
{
    public class Customer : BaseModel
    {
        public string Name { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string Login { get; private set; } = string.Empty;

        public int? IdType { get; private set; }

        private Customer(int id, int? idType, string name, string password, string login)
        {
            Id = id;
            IdType = idType;
            Name = name;
            Password = password;
            Login = login;
        }

        public static Customer Create(int id, int? idType, string name, string password, string login)
        {
            return new Customer(id, idType, name, password, login);
        }
    }
}
