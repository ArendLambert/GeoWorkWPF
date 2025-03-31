using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Abstractions;
using System.Diagnostics;
using Core.Models;

namespace DataAccessLayer.Repositories
{
    public class AccessLevelRepository : BaseRepository<AccessLevel>, IRepository<AccessLevel>
    {
        public AccessLevelRepository(GravitySurveyOnDeleteNoAction context, int userId, UnitOfWork unitOfWork) : base(context, userId, unitOfWork){}

        public async Task<List<AccessLevel>> GetAll()
        {
            var levels = await _context.AccessLevels
                .AsNoTracking()
                .ToListAsync();

            return levels.Select(l => AccessLevel
            .Create(l.IdAccessLevel, l.Name, l.Description))
                .ToList();
        }

        public async Task<AccessLevel?> GetById(int? id)
        {
            AccessLevelEntity? level = await _context.AccessLevels.FindAsync(id);
            if (level != null)
            {
                return AccessLevel.Create(level.IdAccessLevel, level.Name, level.Description);
            }
            return null;
        }

        public async Task Create(AccessLevel entity)
        {
            try
            {
                await _context.AccessLevels.AddAsync(new AccessLevelEntity
                {
                    Name = entity.Name,
                    Description = entity.Description
                });
                await _context.SaveChangesAsync();
                await _unitOfWork.AuditLogsService.AddLog("null", entity.ToString(), "AccessLevel", _userId.ToString(), "Create");
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

        public async Task Update(AccessLevel entity)
        {
            var existingEntity = await _context.AccessLevels.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                await _unitOfWork.AuditLogsService.AddLog(existingEntity.ToString(), entity.ToString(), "AccessLevel", _userId.ToString(), "Update");
                existingEntity.Name = entity.Name;
                existingEntity.Description = entity.Description;
                await _context.SaveChangesAsync();

            }
        }

        public async Task Delete(int id)
        {
            try
            {

                var level = await _context.AccessLevels.FindAsync(id);
                if (level != null)
                {
                    _context.AccessLevels.Remove(level);
                    await _context.SaveChangesAsync();
                    await _unitOfWork.AuditLogsService.AddLog(level.ToString(), "null", "AccessLevel", _userId.ToString(), "Delete");
                    return;
                }
                Debug.WriteLine("Entity not found {AccessLevel repository delete}");
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
    }


}
