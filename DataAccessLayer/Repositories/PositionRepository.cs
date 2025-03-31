using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
    public class PositionRepository : BaseRepository<Position>, IRepository<Position>
    {
        public PositionRepository(GravitySurveyOnDeleteNoAction context, int userId, UnitOfWork unitOfWork) : base(context, userId, unitOfWork) { }

        public async Task Create(Position entity)
        {
            try
            {
                var position = new PositionEntity
                {
                    Name = entity.Name,
                    Salary = entity.Salary,
                    IdAccessLevel = entity.IdAccessLevel
                };

                await _context.Positions.AddAsync(position);
                await _context.SaveChangesAsync();
                await _unitOfWork.AuditLogsService.AddLog("null", entity.ToString(), "Position", _userId.ToString(), "Create");
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
                var position = await _context.Positions.FindAsync(id);
                if (position != null)
                {
                    _context.Positions.Remove(position);
                    await _context.SaveChangesAsync();
                    await _unitOfWork.AuditLogsService.AddLog(position.ToString(), "null", "Position", _userId.ToString(), "Delete");
                    return;
                }
                Debug.WriteLine("Entity not found {Position repository delete}");
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

        public async Task<List<Position>> GetAll()
        {
            var positions = await _context.Positions
                .AsNoTracking()
                .ToListAsync();

            return positions.Select(a => Position
                .Create(a.IdPosition, a.Name, a.Salary, a.IdAccessLevel))
                .ToList();
        }

        public async Task<Position?> GetById(int? id)
        {
            PositionEntity? position = await _context.Positions.FindAsync(id);

            if (position != null)
            {
                return Position.Create(position.IdPosition, position.Name, position.Salary, position.IdAccessLevel);
            }
            return null;
        }

        public async Task Update(Position entity)
        {
            var existingEntity = await _context.Positions.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                await _unitOfWork.AuditLogsService.AddLog(existingEntity.ToString(), entity.ToString(), "Position", _userId.ToString(), "Update");
                existingEntity.Name = entity.Name;
                existingEntity.Salary = entity.Salary;
                existingEntity.IdAccessLevel = entity.IdAccessLevel;
                await _context.SaveChangesAsync();
            }
        }
    }
}
