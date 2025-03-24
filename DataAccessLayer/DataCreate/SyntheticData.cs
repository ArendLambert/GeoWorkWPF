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
        public static void CreateAll()
        {
            CreateAccessLevels();
            CreateAreaCoordinates();
        }

        public static void CreateAccessLevels()
        {
            UnitOfWork.AccessLevelRepository.Create(AccessLevel.Create(0, "Администратор", "Создание проектов, добавление площадей в проект"));
            UnitOfWork.AccessLevelRepository.Create(AccessLevel.Create(0, "Руководитель", "Создание профилей и пикетов, составление отчетов"));
            UnitOfWork.AccessLevelRepository.Create(AccessLevel.Create(0, "Клиент", "Просмотр"));
            UnitOfWork.AccessLevelRepository.Create(AccessLevel.Create(0, "Полевой сотрудник", "Занесение данных о точках"));
        }

        public static void CreateAreaCoordinates()
        {
            UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, 1, 0, 10));
            UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, 1, 0, 0));
            UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, 1, 10, 0));
            UnitOfWork.AreaCoordinateRepository.Create(AreaCoordinate.Create(0, 1, 10, 10));
        }
    }
}
