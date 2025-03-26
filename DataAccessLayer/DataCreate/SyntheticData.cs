﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace Core.DataCreate
{
    public class SyntheticData
    {
        private static readonly Random _random = new Random();

        public static UnitOfWork UnitOfWork { get; private set; } = new UnitOfWork(new GravitySurveyOnDeleteNoAction(), 0);

        public static async Task CreateAll()
        {
            await CreateAccessLevels();
            await CreateEquipments();
            await CreatePositions();
            await CreateEmployees();
            await CreateCustomerTypes();
            await CreateCustomers();   // Для заказчиков и подобных используем фиксированные данные
            await CreateProjects();
            await CreateSquares();
            await CreateAreaCoordinates();
            await CreateProfiles();
            await CreatePickets();       // Здесь будет генерация случайных данных
            await CreatePoints();        // Здесь будет генерация случайных данных
            await CreatePicketCoordinate();
            await CreateProfileCoordinate();
            await CreateReport();
        }

        public static async Task DeleteAll()
        {
            await DeleteReport();
            await DeleteProfileCoordinate();
            await DeletePicketCoordinate();
            await DeleteCustomers();
            await DeleteEmployees();
            await DeletePoints();
            await DeletePickets();
            await DeleteProfiles();
            await DeleteSquares();
            await DeleteProjects();
            await DeleteAreaCoordinates();
            await DeletePositions();
            await DeleteCustomerTypes();
            await DeleteEquipments();
            await DeleteAccessLevels();
            await DeleteAuditLogs();
        }

        public async static Task CreateAccessLevels()
        {
            await UnitOfWork.AccessLevelRepository.Create(AccessLevel.Create(0, "Администратор", "Создание проектов, добавление площадей в проект"));
            await UnitOfWork.AccessLevelRepository.Create(AccessLevel.Create(0, "Руководитель", "Создание профилей и пикетов, составление отчетов"));
            await UnitOfWork.AccessLevelRepository.Create(AccessLevel.Create(0, "Клиент", "Просмотр"));
            await UnitOfWork.AccessLevelRepository.Create(AccessLevel.Create(0, "Полевой сотрудник", "Занесение данных о точках"));
        }

        public async static Task DeleteAccessLevels()
        {
            List<AccessLevel> accessLevels = await UnitOfWork.AccessLevelRepository.GetAll();
            foreach (AccessLevel accessLevel in accessLevels)
            {
                await UnitOfWork.AccessLevelRepository.Delete(accessLevel.Id);
            }
        }

        public async static Task CreateAreaCoordinates()
        {
            List<Square> squares = await UnitOfWork.SquareRepository.GetAll();
            int? idSquare = squares.FirstOrDefault()?.Id;
            if (idSquare == null)
            {
                return;
            }
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, idSquare, 0, 10));
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, idSquare, 0, 0));
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, idSquare, 10, 0));
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, idSquare, 10, 10));
        }

        public async static Task DeleteAreaCoordinates()
        {
            List<AreaCoordinate> areaCoordinates = await UnitOfWork.AreaCoordinateRepository.GetAll();
            foreach (AreaCoordinate areaCoordinate in areaCoordinates)
            {
                await UnitOfWork.AreaCoordinateRepository.Delete(areaCoordinate.Id);
            }
        }

        // Генерация случайных точек
        public async static Task CreatePoints()
        {
            List<Employee> employees = await UnitOfWork.EmployeeRepository.GetAll();
            List<Picket> pickets = await UnitOfWork.PicketRepository.GetAll();
            List<Equipment> equipments = await UnitOfWork.EquipmentRepository.GetAll();
            int? idEmployee = employees.FirstOrDefault()?.Id;
            int? idPicket = pickets.FirstOrDefault()?.Id;
            int? idEquipment = equipments.FirstOrDefault()?.Id;
            if (idEmployee == null || idPicket == null || idEquipment == null)
            {
                return;
            }

            // Создаём 5 точек с рандомными значениями координат и других параметров
            for (int i = 0; i < 5; i++)
            {
                double x = Math.Round(_random.NextDouble() * 10, 2);
                double y = Math.Round(_random.NextDouble() * 10, 2);
                double z = Math.Round(_random.NextDouble() * 10 + 5, 2); // пример смещения
                double deviation = Math.Round(_random.NextDouble() * 0.1, 2);
                double measurement = Math.Round(_random.NextDouble() * 100 + 50, 2);
                DateTime measurementTime = DateTime.Now;

                await UnitOfWork.PointRepository.Create(Point.Create(0, x, y, z, deviation, measurement, measurementTime, idEmployee, idEquipment, idPicket));
            }
        }

        public async static Task DeletePoints()
        {
            List<Point> points = await UnitOfWork.PointRepository.GetAll();
            foreach (Point point in points)
            {
                await UnitOfWork.PointRepository.Delete(point.Id);
            }
        }

        public async static Task CreatePositions()
        {
            List<AccessLevel> accessLevels = await UnitOfWork.AccessLevelRepository.GetAll();
            int? idAccessLevel = accessLevels.FirstOrDefault()?.Id;
            if (idAccessLevel == null)
            {
                return;
            }
            await UnitOfWork.PositionRepository.Create(Position.Create(0, "Администратор", 50000, idAccessLevel));
        }

        public async static Task DeletePositions()
        {
            List<Position> positions = await UnitOfWork.PositionRepository.GetAll();
            foreach (Position position in positions)
            {
                await UnitOfWork.PositionRepository.Delete(position.Id);
            }
        }

        public async static Task CreateCustomerTypes()
        {
            await UnitOfWork.CustomerTypeRepository.Create(CustomerType.Create(0, "Тип 1"));
        }

        public async static Task DeleteCustomerTypes()
        {
            List<CustomerType> customerTypes = await UnitOfWork.CustomerTypeRepository.GetAll();
            foreach (CustomerType customerType in customerTypes)
            {
                await UnitOfWork.CustomerTypeRepository.Delete(customerType.Id);
            }
        }

        // Для заказчиков используем фиксированные данные
        public async static Task CreateCustomers()
        {
            List<CustomerType> customerTypes = await UnitOfWork.CustomerTypeRepository.GetAll();
            int? idCustomerType = customerTypes.FirstOrDefault()?.Id;
            if (idCustomerType == null)
            {
                return;
            }
            await UnitOfWork.CustomerRepository.Create(Customer.Create(0, idCustomerType, "Иванов Иван Иванович", "adminPass", "admin"));
        }

        public async static Task DeleteCustomers()
        {
            List<Customer> customers = await UnitOfWork.CustomerRepository.GetAll();
            foreach (Customer customer in customers)
            {
                await UnitOfWork.CustomerRepository.Delete(customer.Id);
            }
        }

        public async static Task CreateEmployees()
        {
            List<Position> positions = await UnitOfWork.PositionRepository.GetAll();
            int? idPosition = positions.FirstOrDefault()?.Id;
            if (idPosition == null)
            {
                return;
            }
            await UnitOfWork.EmployeeRepository.Create(Employee.Create(0, "Иванов ADMIN Иванович 1122 986311", idPosition, "adminPass", "asdasd"));
        }

        public async static Task DeleteEmployees()
        {
            List<Employee> employees = await UnitOfWork.EmployeeRepository.GetAll();
            foreach (Employee employee in employees)
            {
                await UnitOfWork.EmployeeRepository.Delete(employee.Id);
            }
        }

        public async static Task CreateProjects()
        {
            List<Customer> customers = await UnitOfWork.CustomerRepository.GetAll();
            List<Employee> employees = await UnitOfWork.EmployeeRepository.GetAll();
            int? idCustomer = customers.FirstOrDefault()?.Id;
            int? idEmployee = employees.FirstOrDefault()?.Id;
            if (idCustomer == null || idEmployee == null)
            {
                return;
            }
            await UnitOfWork.ProjectRepository.Create(Project.Create(0, "Проект 1", idCustomer, idEmployee));
        }

        public async static Task DeleteProjects()
        {
            List<Project> projects = await UnitOfWork.ProjectRepository.GetAll();
            foreach (Project project in projects)
            {
                await UnitOfWork.ProjectRepository.Delete(project.Id);
            }
        }

        // Генерация пикетов с использованием случайного номера
        public async static Task CreatePickets()
        {
            List<Profile> profiles = await UnitOfWork.ProfileRepository.GetAll();
            int? idProfile = profiles.FirstOrDefault()?.Id;
            if (idProfile == null)
            {
                return;
            }
            // Создаём пикет с случайным суффиксом в имени
            string picketName = $"Picket {_random.Next(1, 100)}";
            await UnitOfWork.PicketRepository.Create(Picket.Create(0, picketName, idProfile));
        }

        public async static Task DeletePickets()
        {
            List<Picket> pickets = await UnitOfWork.PicketRepository.GetAll();
            foreach (Picket picket in pickets)
            {
                await UnitOfWork.PicketRepository.Delete(picket.Id);
            }
        }

        public async static Task CreateProfiles()
        {
            List<Square> squares = await UnitOfWork.SquareRepository.GetAll();
            int? idSquare = squares.FirstOrDefault()?.Id;
            if (idSquare == null)
            {
                return;
            }
            await UnitOfWork.ProfileRepository.Create(Profile.Create(0, "Profile 1", idSquare));
        }

        public async static Task DeleteProfiles()
        {
            List<Profile> profiles = await UnitOfWork.ProfileRepository.GetAll();
            foreach (Profile profile in profiles)
            {
                await UnitOfWork.ProfileRepository.Delete(profile.Id);
            }
        }

        public async static Task CreateSquares()
        {
            List<Project> projects = await UnitOfWork.ProjectRepository.GetAll();
            int? idProject = projects.FirstOrDefault()?.Id;
            if (idProject == null)
            {
                return;
            }
            await UnitOfWork.SquareRepository.Create(Square.Create(0, "Square 1", 100, idProject));
        }

        public async static Task DeleteSquares()
        {
            List<Square> squares = await UnitOfWork.SquareRepository.GetAll();
            foreach (Square square in squares)
            {
                await UnitOfWork.SquareRepository.Delete(square.Id);
            }
        }

        public async static Task CreateEquipments()
        {
            await UnitOfWork.EquipmentRepository.Create(Equipment.Create(0, "Equipment 1", "123jnsfdij213"));
        }

        public async static Task DeleteEquipments()
        {
            List<Equipment> equipments = await UnitOfWork.EquipmentRepository.GetAll();
            foreach (Equipment equipment in equipments)
            {
                await UnitOfWork.EquipmentRepository.Delete(equipment.Id);
            }
        }

        // Для координат пикетов можно оставить фиксированные данные или добавить рандомизацию по необходимости
        public async static Task CreatePicketCoordinate()
        {
            List<Picket> pickets = await UnitOfWork.PicketRepository.GetAll();
            int? idPicket = pickets.FirstOrDefault()?.Id;
            if (idPicket == null)
            {
                return;
            }
            // Пример с рандомными координатами для пикета
            double x = Math.Round(_random.NextDouble() * 10, 2);
            double y = Math.Round(_random.NextDouble() * 10, 2);
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket, x, y));
        }

        public async static Task DeletePicketCoordinate()
        {
            List<PicketCoordinate> picketCoordinates = await UnitOfWork.PicketCoordinateRepository.GetAll();
            foreach (PicketCoordinate picketCoordinate in picketCoordinates)
            {
                await UnitOfWork.PicketCoordinateRepository.Delete(picketCoordinate.Id);
            }
        }

        public async static Task CreateProfileCoordinate()
        {
            List<Profile> profiles = await UnitOfWork.ProfileRepository.GetAll();
            int? idProfile = profiles.FirstOrDefault()?.Id;
            if (idProfile == null)
            {
                return;
            }
            // Здесь можно также добавить рандомизацию, если это допустимо
            double x = Math.Round(_random.NextDouble() * 10, 2);
            double y = Math.Round(_random.NextDouble() * 10, 2);
            await UnitOfWork.ProfileCoordinateRepository.Create(ProfileCoordinate.Create(0, idProfile, x, y));
        }

        public async static Task DeleteProfileCoordinate()
        {
            List<ProfileCoordinate> profileCoordinates = await UnitOfWork.ProfileCoordinateRepository.GetAll();
            foreach (ProfileCoordinate profileCoordinate in profileCoordinates)
            {
                await UnitOfWork.ProfileCoordinateRepository.Delete(profileCoordinate.Id);
            }
        }

        public async static Task CreateReport()
        {
            List<Employee> employees = await UnitOfWork.EmployeeRepository.GetAll();
            List<Project> projects = await UnitOfWork.ProjectRepository.GetAll();
            int? idEmployee = employees.FirstOrDefault()?.Id;
            int? idProject = projects.FirstOrDefault()?.Id;
            if (idEmployee == null || idProject == null)
            {
                return;
            }
            await UnitOfWork.ReportRepository.Create(Report.Create(0, idEmployee, idProject, "Report 1"));
        }

        public async static Task DeleteReport()
        {
            List<Report> reports = await UnitOfWork.ReportRepository.GetAll();
            foreach (Report report in reports)
            {
                await UnitOfWork.ReportRepository.Delete(report.Id);
            }
        }

        public async static Task DeleteAuditLogs()
        {
            List<AuditLogs> auditLogs = await UnitOfWork.AuditLogRepository.GetAll();
            foreach (AuditLogs auditLog in auditLogs)
            {
                await UnitOfWork.AuditLogRepository.Delete(auditLog.Id);
            }
        }
    }
}
