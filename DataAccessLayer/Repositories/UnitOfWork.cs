using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Services;

namespace DataAccessLayer.Repositories
{
    public class UnitOfWork
    {
        public int UserId { get; private set; }
        public IRepository<AccessLevel> AccessLevelRepository { get; private set; }
        public IRepository<AreaCoordinate> AreaCoordinateRepository { get; private set; }
        public IRepository<Customer> CustomerRepository { get; private set; }
        public IRepository<CustomerType> CustomerTypeRepository { get; private set; }
        public IRepository<Employee> EmployeeRepository { get; private set; }
        public IRepository<Equipment> EquipmentRepository { get; private set; }
        public IRepository<Picket> PicketRepository { get; private set; }
        public IRepository<PicketCoordinate> PicketCoordinateRepository { get; private set; }
        public IRepository<Point> PointRepository { get; private set; }
        public IRepository<Position> PositionRepository { get; private set; }
        public IRepository<Profile> ProfileRepository { get; private set; }
        public IRepository<ProfileCoordinate> ProfileCoordinateRepository { get; private set; }
        public IRepository<Project> ProjectRepository { get; private set; }
        public IRepository<Report> ReportRepository { get; private set; }
        public IRepository<Square> SquareRepository { get; private set; }
        public IRepository<AuditLogs> AuditLogRepository { get; private set; }
        public AuditLogsService AuditLogsService{ get; private set; }
        public UnitOfWork(GravitySurveyOnDeleteNoAction context, int userId)
        {
            UserId = userId;
            AccessLevelRepository = new AccessLevelRepository(context, UserId, this);
            AreaCoordinateRepository = new AreaCoordinateRepository(context, UserId, this);
            CustomerRepository = new CustomerRepository(context, UserId, this);
            CustomerTypeRepository = new CustomerTypeRepository(context, UserId, this);
            EmployeeRepository = new EmployeeRepository(context, UserId, this);
            EquipmentRepository = new EquipmentRepository(context, UserId, this);
            PicketRepository = new PicketRepository(context, UserId, this);
            PicketCoordinateRepository = new PicketCoordinateRepository(context, UserId, this);
            PointRepository = new PointRepository(context, UserId, this);
            PositionRepository = new PositionRepository(context, UserId, this);
            ProfileRepository = new ProfileRepository(context, UserId, this);
            ProfileCoordinateRepository = new ProfileCoordinateRepository(context, UserId, this);
            ProjectRepository = new ProjectRepository(context, UserId, this);
            ReportRepository = new ReportRepository(context, UserId, this);
            SquareRepository = new SquareRepository(context, UserId, this);
            AuditLogRepository = new AuditLogsRepository(context, UserId, this);
            AuditLogsService = new AuditLogsService(context, UserId, this);
        }

    }
}
