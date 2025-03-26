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
    public class EquipmentRepository : BaseRepository<Equipment>, IRepository<Equipment>
    {
        public EquipmentRepository(GravitySurveyOnDeleteNoAction context, int userId, UnitOfWork unitOfWork) : base(context, userId, unitOfWork) { }

        public async Task Create(Equipment entity)
        {
            try
            {
                var equipmentEntity = new EquipmentEntity
                {
                    Name = entity.Name,
                    SerialNumber = entity.SerialNumber
                };

                await _context.Equipment.AddAsync(equipmentEntity);
                await _context.SaveChangesAsync();
                await _unitOfWork.AuditLogsService.AddLog("null", entity.ToString(), "Equipment", _userId.ToString(), "Create");
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
                var equipment = await _context.Equipment.FindAsync(id);
                if (equipment != null)
                {
                    _context.Equipment.Remove(equipment);
                    await _context.SaveChangesAsync();
                    await _unitOfWork.AuditLogsService.AddLog(equipment.ToString(), "null", "Equipment", _userId.ToString(), "Delete");
                    return;
                }
                Debug.WriteLine("Entity not found {Employee repository delete}");
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

        public async Task<List<Equipment>> GetAll()
        {
            var equipment = await _context.Equipment
                .AsNoTracking()
                .ToListAsync();

            return equipment.Select(a => Equipment
                .Create(a.IdEquipment, a.Name, a.SerialNumber))
                .ToList();
        }

        public async Task<Equipment?> GetById(int id)
        {
            EquipmentEntity? equipment = await _context.Equipment.FindAsync(id);

            if (equipment != null)
            {
                return Equipment.Create(equipment.IdEquipment, equipment.Name, equipment.SerialNumber);
            }
            return null;
        }

        public async Task Update(Equipment entity)
        {
            var existingEntity = await _context.Equipment.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                await _unitOfWork.AuditLogsService.AddLog(existingEntity.ToString(), entity.ToString(), "Equipment", _userId.ToString(), "Update");
                existingEntity.SerialNumber = entity.SerialNumber;
                existingEntity.Name = entity.Name;
                await _context.SaveChangesAsync();
            }
        }
    }
}
