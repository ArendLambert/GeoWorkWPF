using System.Windows;
using Core.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace Geo_worker
{
    public partial class EditPicketWindow : Window
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Picket _picket;

        public EditPicketWindow(UnitOfWork unitOfWork, Picket picket)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _picket = picket;

            txtName.Text = _picket.Name;

        }

        private async void EditPicketWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Загрузка данных в cbProfile
            var profiles = await _unitOfWork.ProfileRepository.GetAll();
            cbProfile.ItemsSource = profiles;
            cbProfile.SelectedValue = _picket.IdProfile;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Пожалуйста, введите имя.");
                return;
            }

            var updatedPicket = Picket.Create(
                _picket.Id,
                txtName.Text,
                (int?)cbProfile.SelectedValue
            );

            await _unitOfWork.PicketRepository.Update(updatedPicket);
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