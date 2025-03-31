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
    class ProfileRepository : BaseRepository<Profile>, IRepository<Profile>
    {
        public ProfileRepository(GravitySurveyOnDeleteNoAction context, int userId, UnitOfWork unitOfWork) : base(context, userId, unitOfWork) { }

        public async Task Create(Profile entity)
        {
            try
            {
                var profile= new ProfileEntity
                {
                    IdSquare = entity.IdSquare,
                    Name = entity.Name,
                };

                await _context.Profiles.AddAsync(profile);
                await _context.SaveChangesAsync();
                await _unitOfWork.AuditLogsService.AddLog("null", entity.ToString(), "Profile", _userId.ToString(), "Create");
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
                var profile = await _context.Profiles.FindAsync(id);
                if (profile != null)
                {
                    _context.Profiles.Remove(profile);
                    await _context.SaveChangesAsync();
                    await _unitOfWork.AuditLogsService.AddLog(profile.ToString(), "null", "Profile", _userId.ToString(), "Delete");
                    return;
                }
                Debug.WriteLine("Entity not found {Profile repository delete}");
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

        public async Task<List<Profile>> GetAll()
        {
            var profileCoordinates = await _context.Profiles
                .AsNoTracking()
                .ToListAsync();

            return profileCoordinates.Select(a => Profile
                .Create(a.IdProfile, a.Name, a.IdSquare))
                .ToList();
        }

        public async Task<Profile?> GetById(int? id)
        {
            ProfileEntity? profile = await _context.Profiles.FindAsync(id);

            if (profile != null)
            {
                return Profile.Create(profile.IdProfile, profile.Name, profile.IdSquare);
            }
            return null;
        }

        public async Task Update(Profile entity)
        {
            var existingEntity = await _context.Profiles.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                await _unitOfWork.AuditLogsService.AddLog(existingEntity.ToString(), entity.ToString(), "Profile", _userId.ToString(), "Update");
                existingEntity.Name = entity.Name;
                existingEntity.IdSquare = entity.IdSquare;
                await _context.SaveChangesAsync();
            }
        }
    }
}
