using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace DataAccessLayer.Abstractions
{
    public class BaseRepository<T> where T : class
    {
        private protected readonly GravitySurveyOnDeleteNoAction _context;
        private protected int _userId;
        private protected readonly UnitOfWork _unitOfWork;
        public BaseRepository(GravitySurveyOnDeleteNoAction context, int userId, UnitOfWork unitOfWork)
        {
            _context = context;
            _userId = userId;
            _unitOfWork = unitOfWork;
        }

    }
}
