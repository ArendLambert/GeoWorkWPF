using System.Windows;
using Core.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace Geo_worker
{
    public partial class EditEmployeeWindow : Window
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Employee _employee;

        public EditEmployeeWindow(UnitOfWork unitOfWork, Employee employee)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _employee = employee;

            WinLoaded().GetAwaiter();

            if (_employee != null)
            {
                txtPassport.Text = _employee.Passport;
                txtLogin.Text = _employee.Login;
                txtPassword.Text = _employee.Password;
                cbPosition.SelectedValue = _employee.IdPosition;
            }
        }

        private async Task WinLoaded()
        {
            var positions = await _unitOfWork.PositionRepository.GetAll();
            cbPosition.ItemsSource = positions;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassport.Text) || string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            if (_employee == null)
            {
                var newEmployee = Employee.Create(0, txtPassport.Text, (int?)cbPosition.SelectedValue, txtPassword.Text, txtLogin.Text);
                await _unitOfWork.EmployeeRepository.Create(newEmployee);
            }
            else
            {
                var updatedEmployee = Employee.Create(_employee.Id, txtPassport.Text, (int?)cbPosition.SelectedValue, txtPassword.Text, txtLogin.Text);
                await _unitOfWork.EmployeeRepository.Update(updatedEmployee);
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