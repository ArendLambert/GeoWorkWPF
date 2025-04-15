using System.Windows;
using Core.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace Geo_worker
{
    public partial class EditCustomerTypeWindow : Window
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly CustomerType _customerType;

        public EditCustomerTypeWindow(UnitOfWork unitOfWork, CustomerType customerType)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _customerType = customerType;

            if (_customerType != null)
            {
                txtDescription.Text = _customerType.Description;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Введите описание.");
                return;
            }

            if (_customerType == null)
            {
                var newCustomerType = CustomerType.Create(0, txtDescription.Text);
                await _unitOfWork.CustomerTypeRepository.Create(newCustomerType);
            }
            else
            {
                var updatedCustomerType = CustomerType.Create(_customerType.Id, txtDescription.Text);
                await _unitOfWork.CustomerTypeRepository.Update(updatedCustomerType);
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