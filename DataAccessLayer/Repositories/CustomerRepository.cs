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

namespace DataAccessLayer.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, IRepository<Customer>
    {
        public CustomerRepository(FinallyBoryakin2207g2_GravitySurveyContext context) : base(context)
        {
        }

        public async Task Create(Customer entity)
        {
            await _context.Customers.AddAsync(new CustomerEntity
            {
                IdType = entity.IdType,
            });
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
        }

        public async Task<List<Customer>> GetAll()
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .ToListAsync();

            return customers.Select(a => Customer
            .Create(a.IdCustomer, a.IdType))
                .ToList();
        }

        public async Task<Customer> GetById(int id)
        {
            var customers = await _context.Customers.FindAsync(id);

            return Customer.Create(customers.IdCustomer, customers.IdType);
        }

        public async Task Update(Customer entity)
        {
            var existingEntity = await _context.Customers.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                existingEntity.IdType = entity.IdType;
                await _context.SaveChangesAsync();
            }
        }
    }
}
