using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Core.Models;
using DataAccessLayer.Repositories;

namespace Geo_worker
{
    public partial class ImportProjectWindow : Window
    {
        private UnitOfWork _unitOfWork;
        public ObservableCollection<DisplayProject> Projects { get; set; } = new ObservableCollection<DisplayProject>();
        public DisplayProject? SelectedProject { get; private set; }

        public ImportProjectWindow(int userId)
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork(new DataAccessLayer.Entities.GravitySurveyOnDeleteNoAction(), userId);
            LoadProjects().GetAwaiter();
            DataContext = this;
        }

        private async Task LoadProjects()
        {
            var projects = await _unitOfWork.ProjectRepository.GetAll();

            foreach (var project in projects)
            {
                Customer customer = await _unitOfWork.CustomerRepository.GetById(project.IdCustomer);
                Employee employee = await _unitOfWork.EmployeeRepository.GetById(project.IdEmployee);
                Projects.Add(new DisplayProject(project.Id, project.Name, (int)project.IdCustomer, (int)project.IdEmployee, customer.Name, employee.Passport));
            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectsDataGrid.SelectedItem is DisplayProject project)
            {
                SelectedProject = project;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите проект из списка.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedProject = null;
            DialogResult = false;
            Close();
        }

        public class DisplayProject : Project
        {
            public string CustomerName { get; set; }
            public string EmployeeLogin { get; set; }

            public DisplayProject(int id, string name, int idCustomer, int idEmployee, string customerName, string employeeLogin)
                : base(id, name, idCustomer, idEmployee)
            {
                CustomerName = customerName;
                EmployeeLogin = employeeLogin;
            }

        }
    }
}