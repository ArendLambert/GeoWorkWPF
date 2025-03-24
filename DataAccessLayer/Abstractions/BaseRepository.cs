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
        private protected readonly GravitySurveyOnDeleteNoAction _context;
        public BaseRepository(GravitySurveyOnDeleteNoAction context)
        {
            _context = context;
        }

    }
}
