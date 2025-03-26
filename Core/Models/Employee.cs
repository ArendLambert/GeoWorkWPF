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
        public int Id { get; private set; }

        public string Passport { get; private set; } = string.Empty;

        public int? IdPosition { get; private set; }
        public string Password { get; private set; } = string.Empty;
        public string Login { get; private set; } = string.Empty;

        private Employee(int idEmployee, string passport, int? idPosition, string password, string login)
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
