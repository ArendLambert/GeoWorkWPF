using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Core.Models;
using DataAccessLayer.Repositories;

namespace Geo_worker
{
    /// <summary>
    /// Логика взаимодействия для CreateProjectWindow.xaml
    /// </summary>
    public partial class CreateProjectWindow : Window
    {
        private readonly UnitOfWork _unitOfWork;
        private Project _project;
        public CreateProjectWindow(UnitOfWork unitOfWork, Project project)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _project = project;

            Window_Loaded().GetAwaiter();

            if (_project != null)
            {
                txtName.Text = _project.Name;
                HeadProjectCombobox.SelectedValue = _project.IdEmployee;
                CustomerProjectCombobox.SelectedValue = _project.IdCustomer;
            }
        }

        private async Task Window_Loaded()
        {
            var eployees = await _unitOfWork.EmployeeRepository.GetAll();
            HeadProjectCombobox.ItemsSource = eployees;

            var customers = await _unitOfWork.CustomerRepository.GetAll();
            CustomerProjectCombobox.ItemsSource = customers;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }
            if (_project == null)
            {
                var newProject = Project.Create(0, txtName.Text, (int?)CustomerProjectCombobox.SelectedValue, (int ?)HeadProjectCombobox.SelectedValue);
                await _unitOfWork.ProjectRepository.Create(newProject);
            }
            else
            {
                var updatedProject = Project.Create(_project.Id, txtName.Text, (int?)CustomerProjectCombobox.SelectedValue, (int?)HeadProjectCombobox.SelectedValue);
                await _unitOfWork.ProjectRepository.Update(updatedProject);
            }
            DialogResult = true;
            Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
