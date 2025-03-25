using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using DataAccessLayer.Repositories;

namespace Core.DataCreate
{
    public class SyntheticData
    {
        public static async void CreateAll()
        {
            //await CreateAccessLevels();
            CreateEquipments();
            CreatePositions();
            CreateEmployees();
            CreateCustomerTypes();
            CreateCustomers();
            CreateProjects();
            CreateSquares();
            CreateAreaCoordinates();
            CreateProfiles();
            CreatePickets();
            CreatePoints();
        }

        public async static void CreateAccessLevels()
        {
            await UnitOfWork.AccessLevelRepository.Create(AccessLevel.Create(0, "Администратор", "Создание проектов, добавление площадей в проект"));
            await UnitOfWork.AccessLevelRepository.Create(AccessLevel.Create(0, "Руководитель", "Создание профилей и пикетов, составление отчетов"));
            await UnitOfWork.AccessLevelRepository.Create(AccessLevel.Create(0, "Клиент", "Просмотр"));
            await UnitOfWork.AccessLevelRepository.Create(AccessLevel.Create(0, "Полевой сотрудник", "Занесение данных о точках"));
        }

        public async static void CreateAreaCoordinates()
        {
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, 1, 0, 10));
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, 1, 0, 0));
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, 1, 10, 0));
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, 1, 10, 10));
        }

        public async static void CreatePoints()
        {
            await UnitOfWork.PointRepository.Create(Point.Create(0, 1.1, 2.2, 9.7, 0, 100, DateTime.Now, 1, 1, 12));
        }

        public async static void CreatePositions()
        {
            await UnitOfWork.PositionRepository.Create(Position.Create(0, "Администратор", 50000, 1));
        }

        public async static void CreateCustomerTypes()
        {
            await UnitOfWork.CustomerTypeRepository.Create(CustomerType.Create(0, "Тип 1"));
        }

        public async static void CreateCustomers()
        {
            await UnitOfWork.CustomerRepository.Create(Customer.Create(0, 1, "Иванов Иван Иванович", "adminPass", "admin"));
        }

        public async static void CreateEmployees()
        {
            await UnitOfWork.EmployeeRepository.Create(Employee.Create(0, "Иванов ADMIN Иванович 1122 986311", 1, "adminPass", "asdasd"));
        }

        public async static void CreateProjects()
        {
            await UnitOfWork.ProjectRepository.Create(Project.Create(0, "Проект 1", 1, 1));
        }

        public async static void CreatePickets()
        {
            await UnitOfWork.PicketRepository.Create(Picket.Create(0, "Picket 1", 11));
        }

        public async static void CreateProfiles()
        {
            await UnitOfWork.ProfileRepository.Create(Profile.Create(0, "Profile 1", 10));
        }

        public async static void CreateSquares()
        {
            await UnitOfWork.SquareRepository.Create(Square.Create(0, "Square 1", 100, 7));
        }

        public async static void CreateEquipments()
        {
            await UnitOfWork.EquipmentRepository.Create(Equipment.Create(0, "Equipment 1", "123jnsfdij213"));
        }
    }
}
