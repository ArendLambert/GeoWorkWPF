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
    public class SquareRepository : BaseRepository<Square>, IRepository<Square>
    {
        public SquareRepository(GravitySurveyOnDeleteNoAction context, int userId, UnitOfWork unitOfWork) : base(context, userId, unitOfWork) { }

        public async Task Create(Square entity)
        {
            try
            {
                var project = new SquareEntity
                {
                    Name = entity.Name,
                    Alitude = entity.Alitude,
                    IdProject = entity.IdProject,
                };

                await _context.Squares.AddAsync(project);
                await _context.SaveChangesAsync();
                await _unitOfWork.AuditLogsService.AddLog("null", entity.ToString(), "Square", _userId.ToString(), "Create");
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
                var square = await _context.Squares.FindAsync(id);
                if (square != null)
                {
                    _context.Squares.Remove(square);
                    await _context.SaveChangesAsync();
                    await _unitOfWork.AuditLogsService.AddLog(square.ToString(), "null", "Square", _userId.ToString(), "Delete");
                    return;
                }
                Debug.WriteLine("Entity not found {Square repository delete}");
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

        public async Task<List<Square>> GetAll()
        {
            var square = await _context.Squares
                .AsNoTracking()
                .ToListAsync();

            return square.Select(a => Square
                .Create(a.IdSquare, a.Name, a.Alitude, a.IdProject))
                .ToList();
        }

        public async Task<Square?> GetById(int id)
        {
            SquareEntity? square = await _context.Squares.FindAsync(id);

            if (square != null)
            {
                return Square.Create(square.IdSquare, square.Name, square.Alitude, square.IdProject);
            }
            return null;
        }

        public async Task Update(Square entity)
        {
            var existingEntity = await _context.Squares.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                await _unitOfWork.AuditLogsService.AddLog(existingEntity.ToString(), entity.ToString(), "Square", _userId.ToString(), "Update");
                existingEntity.Alitude = entity.Alitude;
                existingEntity.Name = entity.Name;
                existingEntity.IdProject = entity.IdProject;
                await _context.SaveChangesAsync();
            }
        }
    }
}
