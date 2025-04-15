using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Core.Abstractions;
using Core.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace Geo_worker
{
    public partial class EntityManagementWindow : Window
    {
        private readonly UnitOfWork _unitOfWork;
        private string _selectedEntityType;

        public EntityManagementWindow(UnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            EntityTypeComboBox.SelectedIndex = 0;
        }

        private async void EntityTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EntityTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                _selectedEntityType = selectedItem.Content.ToString();
                await LoadEntities();
            }
        }

        private async Task LoadEntities()
        {
            EntitiesDataGrid.ItemsSource = null;
            switch (_selectedEntityType)
            {
                case "Project":
                    EntitiesDataGrid.ItemsSource = await _unitOfWork.ProjectRepository.GetAll();
                    break;
                case "Customer":
                    EntitiesDataGrid.ItemsSource = await _unitOfWork.CustomerRepository.GetAll();
                    break;
                case "CustomerType":
                    EntitiesDataGrid.ItemsSource = await _unitOfWork.CustomerTypeRepository.GetAll();
                    break;
                case "Employee":
                    EntitiesDataGrid.ItemsSource = await _unitOfWork.EmployeeRepository.GetAll();
                    break;
                case "Equipment":
                    EntitiesDataGrid.ItemsSource = await _unitOfWork.EquipmentRepository.GetAll();
                    break;
                case "Report":
                    EntitiesDataGrid.ItemsSource = await _unitOfWork.ReportRepository.GetAll();
                    break;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Window editWindow = null;
            switch (_selectedEntityType)
            {
                case "Project":
                    editWindow = new CreateProjectWindow(_unitOfWork, null);
                    break;
                case "Customer":
                    editWindow = new EditCustomerWindow(_unitOfWork, null);
                    break;
                case "CustomerType":
                    editWindow = new EditCustomerTypeWindow(_unitOfWork, null);
                    break;
                case "Employee":
                    editWindow = new EditEmployeeWindow(_unitOfWork, null);
                    break;
                case "Equipment":
                    editWindow = new EditEquipmentWindow(_unitOfWork, null);
                    break;
                case "Report":
                    editWindow = new EditReportWindow(_unitOfWork, null);
                    break;
            }

            if (editWindow != null && editWindow.ShowDialog() == true)
            {
                LoadEntities();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (EntitiesDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите сущность для изменения.");
                return;
            }

            Window editWindow = null;
            switch (_selectedEntityType)
            {
                case "Project":
                    editWindow = new CreateProjectWindow(_unitOfWork, (Project)EntitiesDataGrid.SelectedItem);
                    break;
                case "Customer":
                    editWindow = new EditCustomerWindow(_unitOfWork, (Customer)EntitiesDataGrid.SelectedItem);
                    break;
                case "CustomerType":
                    editWindow = new EditCustomerTypeWindow(_unitOfWork, (CustomerType)EntitiesDataGrid.SelectedItem);
                    break;
                case "Employee":
                    editWindow = new EditEmployeeWindow(_unitOfWork, (Employee)EntitiesDataGrid.SelectedItem);
                    break;
                case "Equipment":
                    editWindow = new EditEquipmentWindow(_unitOfWork, (Equipment)EntitiesDataGrid.SelectedItem);
                    break;
                case "Report":
                    editWindow = new EditReportWindow(_unitOfWork, (Report)EntitiesDataGrid.SelectedItem);
                    break;
            }

            if (editWindow != null && editWindow.ShowDialog() == true)
            {
                LoadEntities();
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (EntitiesDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите сущность для удаления.");
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите удалить выбранную сущность?", "Подтверждение удаления", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
                return;

            int id = ((BaseModel)EntitiesDataGrid.SelectedItem).Id;
            switch (_selectedEntityType)
            {
                case "Project":
                    await _unitOfWork.ProjectRepository.Delete(id);
                    break;
                case "Customer":
                    await _unitOfWork.CustomerRepository.Delete(id);
                    break;
                case "CustomerType":
                    await _unitOfWork.CustomerTypeRepository.Delete(id);
                    break;
                case "Employee":
                    await _unitOfWork.EmployeeRepository.Delete(id);
                    break;
                case "Equipment":
                    await _unitOfWork.EquipmentRepository.Delete(id);
                    break;
                case "Report":
                    await _unitOfWork.ReportRepository.Delete(id);
                    break;
            }

            await LoadEntities();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}