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
    public class AuditLogsRepository : BaseRepository<AuditLogs>, IRepository<AuditLogs>
    {
        public AuditLogsRepository(GravitySurveyOnDeleteNoAction context, int userId, UnitOfWork unitOfWork) : base(context, userId, unitOfWork){}

        public async Task Create(AuditLogs entity)
        {
            try
            {
                await _context.AuditLogs.AddAsync(new AuditLogsEntity
                {
                    ChangeDateTime = entity.ChangeDateTime,
                    OldValue = entity.OldValue,
                    NewValue = entity.NewValue,
                    ChangeLocation = entity.ChangeLocation,
                    Username = entity.Username,
                    Action = entity.Action,
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
            try
            {
                var auditLogs = await _context.AuditLogs.FindAsync(id);
                if (auditLogs != null)
                {
                    _context.AuditLogs.Remove(auditLogs);
                    await _context.SaveChangesAsync();
                    return;
                }
                Debug.WriteLine("Entity not found {AuditLogs repository delete}");
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

        public async Task<List<AuditLogs>> GetAll()
        {
            var auditLogs = await _context.AuditLogs
                .AsNoTracking()
                .ToListAsync();

            return auditLogs.Select(a => AuditLogs
                .Create(a.Id, a.ChangeDateTime, a.OldValue, a.NewValue, a.ChangeLocation, a.Username, a.Action))
                .ToList();
        }

        public async Task<AuditLogs?> GetById(int id)
        {
            AuditLogsEntity? auditLogs = await _context.AuditLogs.FindAsync(id);
            if (auditLogs != null)
            {
                return AuditLogs.Create(auditLogs.Id, auditLogs.ChangeDateTime, auditLogs.OldValue, auditLogs.NewValue, auditLogs.ChangeLocation, auditLogs.Username, auditLogs.Action);
            }
            return null;
        }

        public async Task Update(AuditLogs entity)
        {
            var existingEntity = await _context.AuditLogs.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                existingEntity.Action = entity.Action;
                existingEntity.ChangeDateTime = entity.ChangeDateTime;
                existingEntity.ChangeLocation = entity.ChangeLocation;
                existingEntity.NewValue = entity.NewValue;
                existingEntity.OldValue = entity.OldValue;
                existingEntity.Username = entity.Username;
                await _context.SaveChangesAsync();
            }
        }
    }
}
