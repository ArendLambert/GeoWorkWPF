using System.Windows;
using Core.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace Geo_worker
{
    public partial class EditSquareWindow : Window
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Square _square;

        public EditSquareWindow(UnitOfWork unitOfWork, Square square)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _square = square;

            txtName.Text = _square.Name;
            txtAlitude.Text = _square.Alitude.ToString();

        }

        private async void EditSquareWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Загрузка данных в cbProject
            var projects = await _unitOfWork.ProjectRepository.GetAll();
            cbProject.ItemsSource = projects;
            cbProject.SelectedValue = _square.IdProject;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || !int.TryParse(txtAlitude.Text, out int alitude))
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.");
                return;
            }

            var updatedSquare = Square.Create(
                _square.Id,
                txtName.Text,
                alitude,
                (int?)cbProject.SelectedValue
            );

            await _unitOfWork.SquareRepository.Update(updatedSquare);
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}