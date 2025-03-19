using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Abstractions
{
    public class BaseRepository<T> where T : class
    {
        private protected readonly FinallyBoryakin2207g2_GravitySurveyContext _context;
        public BaseRepository(FinallyBoryakin2207g2_GravitySurveyContext context)
        {
            _context = context;
        }

    }
}
