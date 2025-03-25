using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Newtonsoft.Json;



namespace DataAccessLayer.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, IRepository<Customer>
    {
        private readonly GravitySurveyOnDeleteNoAction _context;

        public CustomerRepository(GravitySurveyOnDeleteNoAction context) : base(context)
        {
            _context = context;
        }

        public async Task Create(Customer entity)
        {
            try
            {
                var customerEntity = new CustomerEntity
                {
                    IdType = entity.IdType,
                    Name = entity.Name,
                    Password = entity.Password,
                    Login = entity.Login
                };

                await _context.Customers.AddAsync(customerEntity);
                await _context.SaveChangesAsync();

                // Логируем создание
                await LogChangeAsync("Customer Created", "Insert", null, Newtonsoft.Json.JsonConvert.SerializeObject(customerEntity));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during save: {ex.Message}");
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer != null)
                {
                    var oldValues = Newtonsoft.Json.JsonConvert.SerializeObject(customer);  // Старые значения

                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync();

                    // Логируем удаление
                    await LogChangeAsync("Customer Deleted", "Delete", oldValues, null);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during delete: {ex.Message}");
            }
        }

        public async Task<List<Customer>> GetAll()
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .ToListAsync();

            return customers.Select(a => Customer.Create(a.IdCustomer, a.IdType, a.Name, a.Password, a.Login)).ToList();
        }

        public async Task<Customer?> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                return Customer.Create(customer.IdCustomer, customer.IdType, customer.Name, customer.Password, customer.Login);
            }
            return null;
        }

        public async Task Update(Customer entity)
        {
            var existingEntity = await _context.Customers.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                var oldValues = Newtonsoft.Json.JsonConvert.SerializeObject(existingEntity);  // Старые значения

                existingEntity.IdType = entity.IdType;
                existingEntity.Name = entity.Name;
                existingEntity.Password = entity.Password;
                existingEntity.Login = entity.Login;

                await _context.SaveChangesAsync();

                // Логируем обновление
                await LogChangeAsync("Customer Updated", "Update", oldValues, Newtonsoft.Json.JsonConvert.SerializeObject(entity));
            }
        }

        // Метод для записи изменений в AuditLog
        private async Task LogChangeAsync(string eventType, string action, string? oldValues, string? newValues)
        {
            var auditLog = new AuditLog
            {
                EventType = eventType,
                EntityName = "Customer",  // Указываем, с какой сущностью работаем
                Action = action,
                OldValues = oldValues,
                NewValues = newValues,
                CreatedAt = DateTime.UtcNow
            };

            await _context.AuditLogs.AddAsync(auditLog);
            await _context.SaveChangesAsync();
        }


    }
}