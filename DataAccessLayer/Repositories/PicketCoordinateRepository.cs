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
    public class PicketCoordinateRepository : BaseRepository<PicketCoordinate>, IRepository<PicketCoordinate>
    {
        public PicketCoordinateRepository(GravitySurveyOnDeleteNoAction context, int userId, UnitOfWork unitOfWork) : base(context, userId, unitOfWork) { }

        public async Task Create(PicketCoordinate entity)
        {
            try
            {
                var picketCoordinateEntity = new PicketCoordinateEntity
                {
                    IdPicket = entity.IdPicket,
                    X = entity.X,
                    Y = entity.Y
                };

                await _context.PicketCoordinates.AddAsync(picketCoordinateEntity);
                await _context.SaveChangesAsync();
                await _unitOfWork.AuditLogsService.AddLog("null", entity.ToString(), "PicketCoordinate", _userId.ToString(), "Create");
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
                var picketCoordinate = await _context.PicketCoordinates.FindAsync(id);
                if (picketCoordinate != null)
                {
                    _context.PicketCoordinates.Remove(picketCoordinate);
                    await _context.SaveChangesAsync();
                    await _unitOfWork.AuditLogsService.AddLog(picketCoordinate.ToString(), "null", "PicketCoordinate", _userId.ToString(), "Delete");
                    return;
                }
                Debug.WriteLine("Entity not found {PicketCoordinate repository delete}");
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

        public async Task<List<PicketCoordinate>> GetAll()
        {
            var picketCoordinates = await _context.PicketCoordinates
                .AsNoTracking()
                .ToListAsync();

            return picketCoordinates.Select(a => PicketCoordinate
                .Create(a.IdRecord, a.IdPicket, a.X, a.Y))
                .ToList();
        }

        public async Task<PicketCoordinate?> GetById(int? id)
        {
            PicketCoordinateEntity? picketCoordinate = await _context.PicketCoordinates.FindAsync(id);

            if (picketCoordinate != null)
            {
                return PicketCoordinate.Create(picketCoordinate.IdRecord, picketCoordinate.IdPicket, picketCoordinate.X, picketCoordinate.Y);
            }
            return null;
        }

        public async Task Update(PicketCoordinate entity)
        {
            var existingEntity = await _context.PicketCoordinates.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                await _unitOfWork.AuditLogsService.AddLog(existingEntity.ToString(), entity.ToString(), "PicketCoordinate", _userId.ToString(), "Update");
                existingEntity.IdPicket = entity.IdPicket;
                existingEntity.X = entity.X;
                existingEntity.Y = entity.Y;
                await _context.SaveChangesAsync();
            }
        }
    }
}
