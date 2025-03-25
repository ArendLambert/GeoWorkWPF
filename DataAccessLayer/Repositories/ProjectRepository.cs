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
    public class ProjectRepository : BaseRepository<Project>, IRepository<Project>
    {
        public ProjectRepository(GravitySurveyOnDeleteNoAction context) : base(context)
        {
        }

        public async Task Create(Project entity)
        {
            try
            {
                var project = new ProjectEntity
                {
                    Name = entity.Name,
                    IdCustomer = entity.IdCustomer,
                    IdEmployee = entity.IdEmployee,
                };

                await _context.Projects.AddAsync(project);

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
                var project = await _context.Projects.FindAsync(id);
                if (project != null)
                {
                    _context.Projects.Remove(project);
                    await _context.SaveChangesAsync();
                    return;
                }
                Debug.WriteLine("Entity not found {Project repository delete}");
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

        public async Task<List<Project>> GetAll()
        {
            var projects = await _context.Projects
                .AsNoTracking()
                .ToListAsync();

            return projects.Select(a => Project
                .Create(a.IdProject, a.Name, a.IdCustomer, a.IdEmployee))
                .ToList();
        }

        public async Task<Project?> GetById(int id)
        {
            ProjectEntity? project = await _context.Projects.FindAsync(id);

            if (project != null)
            {
                return Project.Create(project.IdProject, project.Name, project.IdCustomer, project.IdEmployee);
            }
            return null;
        }

        public async Task Update(Project entity)
        {
            var existingEntity = await _context.Projects.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                existingEntity.IdEmployee = entity.IdEmployee;
                existingEntity.Name = entity.Name;
                existingEntity.IdCustomer = entity.IdCustomer;
                await _context.SaveChangesAsync();
            }
        }
    }
}
