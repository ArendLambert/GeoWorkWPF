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
    class PicketRepository : BaseRepository<Picket>, IRepository<Picket>
    {
        public PicketRepository(GravitySurveyOnDeleteNoAction context, int userId, UnitOfWork unitOfWork) : base(context, userId, unitOfWork) { }

        public async Task Create(Picket entity)
        {
            try
            {
                var picket = new PicketEntity
                {
                    IdProfile = entity.IdProfile,
                    Name = entity.Name,
                };

                await _context.Pickets.AddAsync(picket);
                await _context.SaveChangesAsync();
                await _unitOfWork.AuditLogsService.AddLog("null", entity.ToString(), "Picket", _userId.ToString(), "Create");
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
                var picket = await _context.Pickets.FindAsync(id);
                if (picket != null)
                {
                    _context.Pickets.Remove(picket);
                    await _context.SaveChangesAsync();
                    await _unitOfWork.AuditLogsService.AddLog(picket.ToString(), "null", "Picket", _userId.ToString(), "Delete");
                    return;
                }
                Debug.WriteLine("Entity not found {Picket repository delete}");
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

        public async Task<List<Picket>> GetAll()
        {
            var pickets = await _context.Pickets
                .AsNoTracking()
                .ToListAsync();

            return pickets.Select(a => Picket
                .Create(a.IdPicket, a.Name, a.IdProfile))
                .ToList();
        }

        public async Task<Picket?> GetById(int? id)
        {
            PicketEntity? picket = await _context.Pickets.FindAsync(id);

            if (picket != null)
            {
                return Picket.Create(picket.IdPicket, picket.Name, picket.IdProfile);
            }
            return null;
        }

        public async Task Update(Picket entity)
        {
            var existingEntity = await _context.Pickets.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                await _unitOfWork.AuditLogsService.AddLog(existingEntity.ToString(), entity.ToString(), "Picket", _userId.ToString(), "Update");
                existingEntity.IdProfile = entity.IdProfile;
                existingEntity.Name = entity.Name;
                await _context.SaveChangesAsync();
            }
        }
    }
}
