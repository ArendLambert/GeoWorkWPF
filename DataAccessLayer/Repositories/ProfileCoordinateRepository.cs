﻿using System;
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
    public class ProfileCoordinateRepository : BaseRepository<ProfileCoordinate>, IRepository<ProfileCoordinate>
    {
        public ProfileCoordinateRepository(GravitySurveyOnDeleteNoAction context) : base(context)
        {
        }

        public async Task Create(ProfileCoordinate entity)
        {
            try
            {
                var profileCoordinate = new ProfileCoordinateEntity
                {
                    IdProfile = entity.Id,
                    X = entity.X,
                    Y = entity.Y
                };

                await _context.ProfileCoordinates.AddAsync(profileCoordinate);

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
            var profileCoordinate = await _context.ProfileCoordinates.FindAsync(id);
            if (profileCoordinate != null)
            {
                _context.ProfileCoordinates.Remove(profileCoordinate);
                await _context.SaveChangesAsync();
                return;
            }
            Debug.WriteLine("Entity not found {ProfileCoordinate repository delete}");
        }

        public async Task<List<ProfileCoordinate>> GetAll()
        {
            var profileCoordinates = await _context.ProfileCoordinates
                .AsNoTracking()
                .ToListAsync();

            return profileCoordinates.Select(a => ProfileCoordinate
                .Create(a.IdRecord, a.IdProfile, a.X, a.Y))
                .ToList();
        }

        public async Task<ProfileCoordinate?> GetById(int id)
        {
            ProfileCoordinateEntity? profileCoordinate = await _context.ProfileCoordinates.FindAsync(id);

            if (profileCoordinate != null)
            {
                return ProfileCoordinate.Create(profileCoordinate.IdRecord, profileCoordinate.IdProfile, profileCoordinate.X, profileCoordinate.Y);
            }
            return null;
        }

        public async Task Update(ProfileCoordinate entity)
        {
            var existingEntity = await _context.ProfileCoordinates.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                existingEntity.IdProfile = entity.IdProfile;
                existingEntity.X = entity.X;
                existingEntity.Y = entity.Y;
                await _context.SaveChangesAsync();
            }
        }
    }
}
