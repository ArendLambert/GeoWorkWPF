using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class UnitOfWork
    {
        public static IRepository<AccessLevel> AccessLevelRepository { get; } = new AccessLevelRepository(new GravitySurveyOnDeleteNoAction());
        public static IRepository<AreaCoordinate> AreaCoordinateRepository { get; } = new AreaCoordinateRepository(new GravitySurveyOnDeleteNoAction());
        public static IRepository<Customer> CustomerRepository { get; } = new CustomerRepository(new GravitySurveyOnDeleteNoAction());
        public static IRepository<CustomerType> CustomerTypeRepository { get; } = new CustomerTypeRepository(new GravitySurveyOnDeleteNoAction());
        public static IRepository<Employee> EmployeeRepository { get; } = new EmployeeRepository(new GravitySurveyOnDeleteNoAction());
        public static IRepository<Equipment> EquipmentRepository { get; } = new EquipmentRepository(new GravitySurveyOnDeleteNoAction());
        public static IRepository<Picket> PicketRepository { get; } = new PicketRepository(new GravitySurveyOnDeleteNoAction());
        public static IRepository<PicketCoordinate> PicketCoordinateRepository { get; } = new PicketCoordinateRepository(new GravitySurveyOnDeleteNoAction());
        public static IRepository<Point> PointRepository { get; } = new PointRepository(new GravitySurveyOnDeleteNoAction());
        public static IRepository<Position> PositionRepository { get; } = new PositionRepository(new GravitySurveyOnDeleteNoAction());
        public static IRepository<Profile> ProfileRepository { get; } = new ProfileRepository(new GravitySurveyOnDeleteNoAction());
        public static IRepository<ProfileCoordinate> ProfileCoordinateRepository { get; } = new ProfileCoordinateRepository(new GravitySurveyOnDeleteNoAction());
        public static IRepository<Project> ProjectRepository { get; } = new ProjectRepository(new GravitySurveyOnDeleteNoAction());
        public static IRepository<Report> ReportRepository { get; } = new ReportRepository(new GravitySurveyOnDeleteNoAction());
        public static IRepository<Square> SquareRepository { get; } = new SquareRepository(new GravitySurveyOnDeleteNoAction());

    }
}
