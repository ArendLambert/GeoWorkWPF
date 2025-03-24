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
        public SquareRepository(GravitySurveyOnDeleteNoAction context) : base(context)
        {
        }

        public async Task Create(Square entity)
        {
            try
            {
                var project = new SquareEntity
                {
                    Name = entity.Name,
                    Alitude = entity.Alitude,
                    IdProject = entity.Id,
                };

                await _context.Squares.AddAsync(project);

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
            var square = await _context.Squares.FindAsync(id);
            if (square != null)
            {
                _context.Squares.Remove(square);
                await _context.SaveChangesAsync();
                return;
            }
            Debug.WriteLine("Entity not found {Square repository delete}");
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
                existingEntity.Alitude = entity.Alitude;
                existingEntity.Name = entity.Name;
                existingEntity.IdProject = entity.IdProject;
                await _context.SaveChangesAsync();
            }
        }
    }
}
