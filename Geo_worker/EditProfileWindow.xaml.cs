using System.Windows;
using Core.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace Geo_worker
{
    public partial class EditProfileWindow : Window
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Profile _profile;

        public EditProfileWindow(UnitOfWork unitOfWork, Profile profile)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _profile = profile;

            txtName.Text = _profile.Name;

        }

        private async void EditProfileWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Загрузка данных в cbSquare
            var squares = await _unitOfWork.SquareRepository.GetAll();
            cbSquare.ItemsSource = squares;
            cbSquare.SelectedValue = _profile.IdSquare;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Пожалуйста, введите имя.");
                return;
            }

            var updatedProfile = Profile.Create(
                _profile.Id,
                txtName.Text,
                (int?)cbSquare.SelectedValue
            );

            await _unitOfWork.ProfileRepository.Update(updatedProfile);
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void EditSquareWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}