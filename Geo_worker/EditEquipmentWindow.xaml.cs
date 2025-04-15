using System.Windows;
using Core.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace Geo_worker
{
    public partial class EditEquipmentWindow : Window
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Equipment _equipment;

        public EditEquipmentWindow(UnitOfWork unitOfWork, Equipment equipment)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _equipment = equipment;

            if (_equipment != null)
            {
                txtName.Text = _equipment.Name;
                txtSerialNumber.Text = _equipment.SerialNumber;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSerialNumber.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            if (_equipment == null)
            {
                var newEquipment = Equipment.Create(0, txtName.Text, txtSerialNumber.Text);
                await _unitOfWork.EquipmentRepository.Create(newEquipment);
            }
            else
            {
                var updatedEquipment = Equipment.Create(_equipment.Id, txtName.Text, txtSerialNumber.Text);
                await _unitOfWork.EquipmentRepository.Update(updatedEquipment);
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