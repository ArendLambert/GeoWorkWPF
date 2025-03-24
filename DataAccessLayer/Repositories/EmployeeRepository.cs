using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Geo_worker.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IRepository<Employee>
    {
        public EmployeeRepository(GravitySurveyOnDeleteNoAction context) : base(context)
        {
        }

        public async Task Create(Employee entity)
        {
            try
            {
                var employeeEntity = new EmployeeEntity
                {
                    Passport = entity.Passport,
                    IdPosition = entity.IdPosition
                };

                await _context.Employees.AddAsync(employeeEntity);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during save: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    Debug.WriteLine($"Stack trace: {ex.InnerException.StackTrace}");
                }
            }
        }

        public async Task Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return;
            }
            Debug.WriteLine("Entity not found {Employee repository delete}");
        }

        public async Task<List<Employee>> GetAll()
        {
            var customerTypes = await _context.Employees
                .AsNoTracking()
                .ToListAsync();

            return customerTypes.Select(a => Employee
                .Create(a.IdEmployee, a.Passport, a.IdPosition))
                .ToList();
        }

        public async Task<Employee?> GetById(int id)
        {
            EmployeeEntity? employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                return Employee.Create(employee.IdEmployee, employee.Passport, employee.IdPosition);
            }
            return null;
        }

        public async Task Update(Employee entity)
        {
            var existingEntity = await _context.Employees.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                existingEntity.Passport = entity.Passport;
                existingEntity.IdPosition = entity.IdPosition;
                await _context.SaveChangesAsync();
            }
        }
    }
}
