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

namespace DataAccessLayer.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, IRepository<Customer>
    {
        public CustomerRepository(GravitySurveyOnDeleteNoAction context) : base(context){}

        public async Task Create(Customer entity)
        {
            try
            {
                await _context.Customers.AddAsync(new CustomerEntity
                {
                    IdType = entity.IdType,
                    Name = entity.Name,
                    Password = entity.Password,
                    Login = entity.Login
                });
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
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return;
            }
            Debug.WriteLine("Entity not found {Customer repository delete}");
        }

        public async Task<List<Customer>> GetAll()
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .ToListAsync();

            return customers.Select(a => Customer
            .Create(a.IdCustomer, a.IdType, a.Name, a.Password, a.Login))
                .ToList();
        }

        public async Task<Customer?> GetById(int id)
        {
            CustomerEntity? customer = await _context.Customers.FindAsync(id);
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
                existingEntity.IdType = entity.IdType;
                existingEntity.Name = entity.Name;
                existingEntity.Password = entity.Password;
                existingEntity.Login = entity.Login;
                await _context.SaveChangesAsync();
            }
        }
    }
}
