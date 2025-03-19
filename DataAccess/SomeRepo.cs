using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Geo_worker.Core.Models;

namespace DataAccess
{
    public class SomeRepo
    {
        private readonly FinallyBoryakin2207g2_GravitySurveyContext _context;
        public SomeRepo(FinallyBoryakin2207g2_GravitySurveyContext context)
        {
            _context = context;
        }

        public int getNumber()
        {
            return _context.AccessLevels.Count();
        }

        public void AddAccessLevel(string name, string description) 
        {
            _context.AccessLevels.Add(new AccessLevel
            {
                Name = name,
                Description = description
            });
            _context.SaveChanges();
        }

        public List<AccessLevel> GetAccessLevels()
        {
            var levels = _context.AccessLevels
                .AsNoTracking()
                .ToList();
            return levels.Select(l => new AccessLevelModel
            {
                IdAccessLevel = l.IdAccessLevel,
                Name = l.Name,
                Description = l.Description
            }).ToList();
        }
    }


}
