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
    public class AreaCoordinateRepository : BaseRepository<AreaCoordinate>, IRepository<AreaCoordinate>
    {
        public AreaCoordinateRepository(GravitySurveyOnDeleteNoAction context) : base(context){}

        public async Task Create(AreaCoordinate entity)
        {
            try
            {
                await _context.AreaCoordinates.AddAsync(new AreaCoordinateEntity
                {
                    IdSquare = entity.IdSquare,
                    X = entity.X,
                    Y = entity.Y,
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
                var areaCoordinates = await _context.AreaCoordinates.FindAsync(id);
                if (areaCoordinates != null)
                {
                    _context.AreaCoordinates.Remove(areaCoordinates);
                    await _context.SaveChangesAsync();
                    return;
                }
                Debug.WriteLine("Entity not found {AreaCoordinates repository delete}");
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

        public async Task<List<AreaCoordinate>> GetAll()
        {
            var areaCoordinates = await _context.AreaCoordinates
                .AsNoTracking()
                .ToListAsync();

            return areaCoordinates.Select(a => AreaCoordinate
            .Create(a.IdRecord, a.IdSquare, a.X, a.Y))
                .ToList();
        }

        public async Task<AreaCoordinate?> GetById(int id)
        {
            AreaCoordinateEntity? areaCoordinates = await _context.AreaCoordinates.FindAsync(id);
            if (areaCoordinates != null)
            {
                return AreaCoordinate.Create(areaCoordinates.IdRecord, areaCoordinates.IdSquare, areaCoordinates.X, areaCoordinates.Y);
            }
            return null;
        }

        public async Task Update(AreaCoordinate entity)
        {
            var existingEntity = await _context.AreaCoordinates.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                existingEntity.IdSquare = entity.IdSquare;
                existingEntity.X = entity.X;
                existingEntity.Y = entity.Y;
                await _context.SaveChangesAsync();
            }
        }
    }
}
