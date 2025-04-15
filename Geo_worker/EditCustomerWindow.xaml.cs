using System.Windows;
using Core.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace Geo_worker
{
    public partial class EditCustomerWindow : Window
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Customer _customer;

        public EditCustomerWindow(UnitOfWork unitOfWork, Customer customer)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _customer = customer;

            Window_Loaded().GetAwaiter();

            if (_customer != null)
            {
                txtName.Text = _customer.Name;
                txtLogin.Text = _customer.Login;
                txtPassword.Text = _customer.Password;
                cbCustomerType.SelectedValue = _customer.IdType;
            }
        }

        private async Task Window_Loaded()
        {
            var customerTypes = await _unitOfWork.CustomerTypeRepository.GetAll();
            cbCustomerType.ItemsSource = customerTypes;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            if (_customer == null)
            {
                var newCustomer = Customer.Create(0, (int?)cbCustomerType.SelectedValue, txtName.Text, txtPassword.Text, txtLogin.Text);
                await _unitOfWork.CustomerRepository.Create(newCustomer);
            }
            else
            {
                var updatedCustomer = Customer.Create(_customer.Id, (int?)cbCustomerType.SelectedValue, txtName.Text, txtPassword.Text, txtLogin.Text);
                await _unitOfWork.CustomerRepository.Update(updatedCustomer);
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