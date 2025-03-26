using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace DataAccessLayer.Services
{
    public class AuditLogsService
    {
        private AuditLogsRepository _auditLogsRepository;

        public AuditLogsService(GravitySurveyOnDeleteNoAction context, int userId, UnitOfWork unitOfWork)
        {
            _auditLogsRepository = new AuditLogsRepository(context, userId, unitOfWork);
        }

        public async Task AddLog(string oldValue, string newValue, string changeLocation, string username, string action)
        {
            if(oldValue == null)
            {
                oldValue = "null";
            }
            if(newValue == null) {
                newValue = "null";
            }
            await _auditLogsRepository.Create(Core.Models.AuditLogs.Create(0, DateTime.Now, oldValue, newValue, changeLocation, username, action));
        }
    }
}
