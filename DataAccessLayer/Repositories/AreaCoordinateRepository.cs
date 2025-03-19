using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Geo_worker.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class AreaCoordinateRepository : BaseRepository<AreaCoordinate>, IRepository<AreaCoordinate>
    {
        public AreaCoordinateRepository(FinallyBoryakin2207g2_GravitySurveyContext context) : base(context)
        {
        }

        public async Task Create(AreaCoordinate entity)
        {
            await _context.AreaCoordinates.AddAsync(new AreaCoordinateEntity
            {
                IdSquare = entity.IdSquare,
                X = entity.X,
                Y = entity.Y,
            });
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var areaCoordinates = await _context.AreaCoordinates.FindAsync(id);
            _context.AreaCoordinates.Remove(areaCoordinates);
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

        public async Task<AreaCoordinate> GetById(int id)
        {
            var areaCoordinates = await _context.AreaCoordinates.FindAsync(id);

            return AreaCoordinate.Create(areaCoordinates.IdRecord, areaCoordinates.IdSquare, areaCoordinates.X, areaCoordinates.Y);
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
