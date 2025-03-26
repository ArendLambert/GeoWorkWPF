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
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class CustomerTypeRepository : BaseRepository<CustomerType>, IRepository<CustomerType>
    {
        public CustomerTypeRepository(GravitySurveyOnDeleteNoAction context, int userId, UnitOfWork unitOfWork) : base(context, userId, unitOfWork) { }

        public async Task Create(CustomerType entity)
        {
            try
            {
                await _context.CustomerTypes.AddAsync(new CustomerTypeEntity
                {
                    Description = entity.Description
                });
                await _context.SaveChangesAsync();
                await _unitOfWork.AuditLogsService.AddLog("null", entity.ToString(), "CustomerType", _userId.ToString(), "Create");
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
            try
            {
                var customerType = await _context.CustomerTypes.FindAsync(id);
                if (customerType != null)
                {
                    _context.CustomerTypes.Remove(customerType);
                    await _context.SaveChangesAsync();
                    await _unitOfWork.AuditLogsService.AddLog(customerType.ToString(), "null", "CustomerType", _userId.ToString(), "Delete");
                    return;
                }
                Debug.WriteLine("Entity not found {CustomerType repository delete}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during deleting: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    Debug.WriteLine($"Stack trace: {ex.InnerException.StackTrace}");
                }
            }
        }

        public async Task<List<CustomerType>> GetAll()
        {
            var customerTypes = await _context.CustomerTypes
                .AsNoTracking()
                .ToListAsync();

            return customerTypes.Select(a => CustomerType
                .Create(a.IdCustomerType, a.Description))
                .ToList();
        }

        public async Task<CustomerType?> GetById(int id)
        {
            CustomerTypeEntity? customerType = await _context.CustomerTypes.FindAsync(id);
            if (customerType != null)
            {
                return CustomerType.Create(customerType.IdCustomerType, customerType.Description);
            }
            return null;

            
        }

        public async Task Update(CustomerType entity)
        {
            var existingEntity = await _context.CustomerTypes.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                await _unitOfWork.AuditLogsService.AddLog(existingEntity.ToString(), entity.ToString(), "CustomerType", _userId.ToString(), "Update");
                existingEntity.Description = entity.Description;
                await _context.SaveChangesAsync();
            }
        }
    }
}
