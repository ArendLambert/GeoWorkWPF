using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;

namespace Core.Models
{
    public class Employee : BaseModel
    {

        public string Passport { get; protected set; } = string.Empty;

        public int? IdPosition { get; protected set; }
        public string Password { get; protected set; } = string.Empty;
        public string Login { get; protected set; } = string.Empty;

        protected Employee(int idEmployee, string passport, int? idPosition, string password, string login)
        {
            Id = idEmployee;
            Passport = passport;
            IdPosition = idPosition;
            Login = login;
            Password = password;
        }

        public static Employee Create(int idEmployee, string passport, int? idPosition, string password, string login)
        {
            return new Employee(idEmployee, passport, idPosition, password, login);
        }
    }
}
