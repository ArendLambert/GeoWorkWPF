using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static UnitOfWork UnitOfWork { get; private set; } = new UnitOfWork(new GravitySurveyOnDeleteNoAction(), 0);
        public static async Task CreateAll()
        {
            await CreateAccessLevels();
            await CreateEquipments();
            await CreatePositions();
            await CreateEmployees();
            await CreateCustomerTypes();
            await CreateCustomers();
            await CreateProjects();
            await CreateSquares();
            await CreateAreaCoordinates();
            await CreateProfiles();
            await CreatePickets();
            await CreatePoints();
            await CreatePicketCoordinate();
            await CreateProfileCoordinate();
            await CreateReport();
        }

        public static async Task DeleteAll()
        {
            await DeleteAuditLogs();
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
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, idSquare, 100, 100));
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, idSquare, 300, 100));
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, idSquare, 300, 200));
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, idSquare, 200, 300));
            //await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, idSquare, 100, 100));

            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, idSquare + 1, 300, 100));
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, idSquare + 1, 300, 200));
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, idSquare + 1, 200, 300));
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, idSquare+1, 400, 300));
            await UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, idSquare+1, 400, 100));
        }

        public async static Task DeleteAreaCoordinates()
        {
            List<AreaCoordinate> areaCoordinates = await UnitOfWork.AreaCoordinateRepository.GetAll();
            foreach (AreaCoordinate areaCoordinate in areaCoordinates)
            {
                await UnitOfWork.AreaCoordinateRepository.Delete(areaCoordinate.Id);
            }
        }

        public async static Task CreatePoints()
        {
            List<Employee> employees = await UnitOfWork.EmployeeRepository.GetAll();
            List<Picket> pickets = await UnitOfWork.PicketRepository.GetAll();
            List<Equipment> equipments = await UnitOfWork.EquipmentRepository.GetAll();
            int? idEmployee = employees.FirstOrDefault()?.Id;
            int? idPicket = pickets.FirstOrDefault()?.Id;
            int? idSecondPicket = pickets.ElementAtOrDefault(1)?.Id;
            int? idEquipment = equipments.FirstOrDefault()?.Id;
            if (idEmployee == null || idPicket == null || idEquipment == null)
            {
                return;
            }
            await UnitOfWork.PointRepository.Create(Point.Create(0, 178.0, 112.0, 9.7, 0, 100.3, DateTime.Now, idEmployee, idEquipment, idPicket));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 180.0, 124.0, 9.75, 0.05, 95.7, DateTime.Now, idEmployee, idEquipment, idPicket));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 188.0, 121.0, 9.7, 0, 102.3, DateTime.Now, idEmployee, idEquipment, idPicket));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 191.0, 133.0, 9.72, 0.02, 98.1, DateTime.Now, idEmployee, idEquipment, idPicket));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 199.0, 132.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 213.0, 138.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 212.0, 130.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 221.0, 131.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 223.0, 120.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 233.0, 126.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 243.0, 131.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 242.0, 140.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket));

            await UnitOfWork.PointRepository.Create(Point.Create(0, 150.0, 154.0, 9.7, 0, 100.3, DateTime.Now, idEmployee, idEquipment, idPicket+1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 154.0, 159.0, 9.75, 0.05, 95.7, DateTime.Now, idEmployee, idEquipment, idPicket+1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 152.0, 166.0, 9.7, 0, 102.3, DateTime.Now, idEmployee, idEquipment, idPicket + 1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 155.0, 169.0, 9.72, 0.02, 98.1, DateTime.Now, idEmployee, idEquipment, idPicket + 1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 158.0, 174.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 164.0, 177.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 168.0, 174.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 175.0, 176.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 178.0, 182.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 175.0, 185.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 176.0, 193.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 172.0, 198.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 180.0, 206.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 188.0, 200.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 200.0, 204.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 208.0, 213.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 1));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 200.0, 224.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 1));

            await UnitOfWork.PointRepository.Create(Point.Create(0, 277.0, 270.0, 9.7, 0, 100.3, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 282, 271.0, 9.75, 0.05, 95.7, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 283, 276.0, 9.7, 0, 102.3, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 287.0, 277.0, 9.72, 0.02, 98.1, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 293.0, 281.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 294.0, 276.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 299.0, 276.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 298.0, 271.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 303.0, 270.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 308.0, 271.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 309.0, 276.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 314.0, 276.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 317.0, 282.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 322.0, 280.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 326.0, 277.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 325.0, 274.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 327.0, 269.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 332.0, 270.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 336.0, 267.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 338.0, 275.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 343.0, 275.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 346.0, 282.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 351.0, 280.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 356.0, 279.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 356.0, 273.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 361.0, 271.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
            await UnitOfWork.PointRepository.Create(Point.Create(0, 360.0, 268.0, 9.65, -0.05, 102.4, DateTime.Now, idEmployee, idEquipment, idPicket + 2));
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

        public async static Task CreatePickets()
        {
            List<Profile> profiles = await UnitOfWork.ProfileRepository.GetAll();
            int? idProfile = profiles.FirstOrDefault()?.Id;
            if (idProfile == null)
            {
                return;
            }
            await UnitOfWork.PicketRepository.Create(Picket.Create(0, "Picket 1", idProfile));
            await UnitOfWork.PicketRepository.Create(Picket.Create(0, "Picket 2", idProfile+1));
            await UnitOfWork.PicketRepository.Create(Picket.Create(0, "Picket 3", idProfile+2));
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
            await UnitOfWork.ProfileRepository.Create(Profile.Create(0, "Profile 2", idSquare));
            await UnitOfWork.ProfileRepository.Create(Profile.Create(0, "Profile 3", idSquare+1));
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
            await UnitOfWork.SquareRepository.Create(Square.Create(0, "Square 2", 120, idProject));
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

        public async static Task CreatePicketCoordinate()
        {
            List<Picket> pickets = await UnitOfWork.PicketRepository.GetAll();
            int? idPicket = pickets.FirstOrDefault()?.Id;
            if (idPicket == null)
            {
                return;
            }
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket, 170.0, 110.0));
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket, 205.0, 140.0));
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket, 230.0, 115.0));
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket, 250.0, 150.0));

            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket+1, 150.0, 150.0));
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket+1, 155.0, 175.0));
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket+1, 180.0, 175.0));
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket+1, 175.0, 205.0));
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket+1, 205.0, 200.0));
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket+1, 200.0, 235.0));

            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket + 2, 275.0, 265.0));
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket + 2, 290.0, 280.0));
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket + 2, 300.0, 265.0));
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket + 2, 320.0, 285.0));
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket + 2, 330.0, 265.0));
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket + 2, 350.0, 285.0));
            await UnitOfWork.PicketCoordinateRepository.Create(PicketCoordinate.Create(0, idPicket + 2, 360.0, 265.0));
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
            await UnitOfWork.ProfileCoordinateRepository.Create(ProfileCoordinate.Create(0, idProfile, 100.0, 100.0));
            await UnitOfWork.ProfileCoordinateRepository.Create(ProfileCoordinate.Create(0, idProfile, 300.0, 100.0));
            await UnitOfWork.ProfileCoordinateRepository.Create(ProfileCoordinate.Create(0, idProfile, 300.0, 200.0));

            await UnitOfWork.ProfileCoordinateRepository.Create(ProfileCoordinate.Create(0, idProfile+1, 100.0, 100.0));
            await UnitOfWork.ProfileCoordinateRepository.Create(ProfileCoordinate.Create(0, idProfile+1, 200.0, 300.0));
            await UnitOfWork.ProfileCoordinateRepository.Create(ProfileCoordinate.Create(0, idProfile+1, 300.0, 200.0));

            await UnitOfWork.ProfileCoordinateRepository.Create(ProfileCoordinate.Create(0, idProfile + 2, 200.0, 300.0));
            await UnitOfWork.ProfileCoordinateRepository.Create(ProfileCoordinate.Create(0, idProfile + 2, 400.0, 300.0));
            await UnitOfWork.ProfileCoordinateRepository.Create(ProfileCoordinate.Create(0, idProfile + 2, 400.0, 100.0));
            await UnitOfWork.ProfileCoordinateRepository.Create(ProfileCoordinate.Create(0, idProfile + 2, 300.0, 100.0));
            await UnitOfWork.ProfileCoordinateRepository.Create(ProfileCoordinate.Create(0, idProfile + 2, 300.0, 200.0));
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
            Debug.WriteLine("!!!!!!!!!!!!!!!!!Удаление AuditLogs!!!!!!!!!!!!!!!!!!");
            try
            {
                List<AuditLogs> auditLogs = await UnitOfWork.AuditLogRepository.GetAll();
                foreach (AuditLogs auditLog in auditLogs)
                {
                    try
                    {
                        await UnitOfWork.AuditLogRepository.Delete(auditLog.Id);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Ошибка при удалении AuditLog с ID {auditLog.Id}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при получении AuditLogs: {ex.Message}");
            }
        }
    }
}
