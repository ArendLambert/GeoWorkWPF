using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Geo_worker.Core.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Abstractions;

namespace DataAccessLayer.Repositories
{
    public class AccessLevelRepository : BaseRepository<AccessLevel>, IRepository<AccessLevel>
    {
        public AccessLevelRepository(FinallyBoryakin2207g2_GravitySurveyContext context) : base(context){}

        public async Task<List<AccessLevel>> GetAll()
        {
            var levels = await _context.AccessLevels
                .AsNoTracking()
                .ToListAsync();

            return levels.Select(l => AccessLevel
            .Create(l.IdAccessLevel, l.Name, l.Description))
                .ToList();
        }

        public async Task<AccessLevel> GetById(int id)
        {
            var level = await _context.AccessLevels.FindAsync(id);

            return AccessLevel.Create(level.IdAccessLevel, level.Name, level.Description);
        }

        public async Task Create(AccessLevel entity)
        {
            await _context.AccessLevels.AddAsync(new AccessLevelEntity
            {
                Name = entity.Name,
                Description = entity.Description
            });
            await _context.SaveChangesAsync();
        }

        public async Task Update(AccessLevel entity)
        {
            var existingEntity = await _context.AccessLevels.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                existingEntity.Name = entity.Name;
                existingEntity.Description = entity.Description;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var level = await _context.AccessLevels.FindAsync(id);
            _context.AccessLevels.Remove(level);
        }
    }


}
