using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Employee
    {
        public int Id { get; private set; }

        public string? Passport { get; private set; } = string.Empty;

        public int? IdPosition { get; private set; }

        private Employee(int idEmployee, string? passport, int? idPosition)
        {
            Id = idEmployee; Passport = passport; IdPosition = idPosition;
        }

        public static Employee Create(int idEmployee, string? passport, int? idPosition)
        {
            return new Employee(idEmployee, passport, idPosition);
        }
    }
}
