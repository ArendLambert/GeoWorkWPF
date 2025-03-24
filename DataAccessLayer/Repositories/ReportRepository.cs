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
    class ReportRepository : BaseRepository<Report>, IRepository<Report>
    {
        public ReportRepository(GravitySurveyOnDeleteNoAction context) : base(context)
        {
        }

        public async Task Create(Report entity)
        {
            try
            {
                var report = new ReportEntity
                {
                    IdEmployee = entity.IdEmployee,
                    IdProject = entity.IdProject,
                    ReportFile = entity.ReportFile,
                };

                await _context.Reports.AddAsync(report);

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
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
                return;
            }
            Debug.WriteLine("Entity not found {Report repository delete}");
        }

        public async Task<List<Report>> GetAll()
        {
            var projects = await _context.Reports
                .AsNoTracking()
                .ToListAsync();

            return projects.Select(a => Report
                .Create(a.IdReport, a.IdEmployee, a.IdProject, a.ReportFile))
                .ToList();
        }

        public async Task<Report?> GetById(int id)
        {
            ReportEntity? report = await _context.Reports.FindAsync(id);

            if (report != null)
            {
                return Report.Create(report.IdReport, report.IdEmployee, report.IdProject, report.ReportFile);
            }
            return null;
        }

        public async Task Update(Report entity)
        {
            var existingEntity = await _context.Reports.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                existingEntity.IdEmployee = entity.IdEmployee;
                existingEntity.IdProject = entity.IdProject;
                existingEntity.ReportFile = entity.ReportFile;
                await _context.SaveChangesAsync();
            }
        }
    }
}
