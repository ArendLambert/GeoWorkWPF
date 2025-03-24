using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Core.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class PointRepository : BaseRepository<Core.Models.Point>, IRepository<Core.Models.Point>
    {
        public PointRepository(GravitySurveyOnDeleteNoAction context) : base(context)
        {
        }

        public async Task Create(Core.Models.Point entity)
        {
            try
            {
                var point = new PointEntity
                {
                    X = entity.X,
                    Y = entity.Y,
                    Gravity = entity.Gravity,
                    GravityAnomaly = entity.GravityAnomaly,
                    Amendments = entity.Amendments,
                    Datetime = entity.Datetime,
                    IdOperator = entity.IdOperator,
                    IdEquipment = entity.IdEquipment,
                    IdPicket = entity.IdPicket,

                };

                await _context.Points.AddAsync(point);

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
            var point = await _context.Points.FindAsync(id);
            if (point != null)
            {
                _context.Points.Remove(point);
                await _context.SaveChangesAsync();
                return;
            }
            Debug.WriteLine("Entity not found {Point repository delete}");
        }

        public async Task<List<Core.Models.Point>> GetAll()
        {
            var points = await _context.Points
                .AsNoTracking()
                .ToListAsync();

            return points.Select(a => Core.Models.Point
                .Create(a.IdPoint, a.X, a.Y, a.Gravity, a.GravityAnomaly, a.Amendments, a.Datetime, a.IdOperator, a.IdEquipment, a.IdPicket))
                .ToList();
        }

        public async Task<Core.Models.Point?> GetById(int id)
        {
            PointEntity? picket = await _context.Points.FindAsync(id);

            if (picket != null)
            {
                return Core.Models.Point.Create(picket.IdPoint, picket.X, picket.Y, picket.Gravity, picket.GravityAnomaly, picket.Amendments, picket.Datetime, picket.IdOperator, picket.IdEquipment, picket.IdPicket);
            }
            return null;
        }


        public async Task Update(Core.Models.Point entity)
        {
            var existingEntity = await _context.Points.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                existingEntity.X = entity.X;
                existingEntity.Y = entity.Y;
                existingEntity.Gravity = entity.Gravity;
                existingEntity.GravityAnomaly = entity.GravityAnomaly;
                existingEntity.Amendments = entity.Amendments;
                existingEntity.Datetime = entity.Datetime;
                existingEntity.IdOperator = entity.IdOperator;
                existingEntity.IdEquipment = entity.IdEquipment;
                existingEntity.IdPicket = entity.IdPicket;
                await _context.SaveChangesAsync();
            }
        }
    }
}
