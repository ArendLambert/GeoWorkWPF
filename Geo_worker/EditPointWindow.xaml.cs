using System;
using System.Windows;
using Core.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace Geo_worker
{
    public partial class EditPointWindow : Window
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Core.Models.Point _point;

        public EditPointWindow(UnitOfWork unitOfWork, Core.Models.Point point)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _point = point;

            txtX.Text = _point.X.ToString();
            txtY.Text = _point.Y.ToString();
            txtGravity.Text = _point.Gravity.ToString();
            txtGravityAnomaly.Text = _point.GravityAnomaly.ToString();
            txtAmendments.Text = _point.Amendments.ToString();
            dpDatetime.SelectedDate = _point.Datetime;

        }

        private async void EditPointWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Загрузка данных в cbOperator, cbEquipment и cbPicket
            var operators = await _unitOfWork.EmployeeRepository.GetAll();
            cbOperator.ItemsSource = operators;
            var equipment = await _unitOfWork.EquipmentRepository.GetAll();
            cbEquipment.ItemsSource = equipment;
            var pickets = await _unitOfWork.PicketRepository.GetAll();
            cbPicket.ItemsSource = pickets;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(txtX.Text, out double x) ||
                !double.TryParse(txtY.Text, out double y) ||
                !double.TryParse(txtGravity.Text, out double gravity) ||
                !double.TryParse(txtGravityAnomaly.Text, out double gravityAnomaly) ||
                !double.TryParse(txtAmendments.Text, out double amendments))
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения.");
                return;
            }

            var updatedPoint = Core.Models.Point.Create(
                _point.Id,
                x,
                y,
                gravity,
                gravityAnomaly,
                amendments,
                dpDatetime.SelectedDate ?? DateTime.Now,
                (int?)cbOperator.SelectedValue,
                (int?)cbEquipment.SelectedValue,
                (int?)cbPicket.SelectedValue
            );

            await _unitOfWork.PointRepository.Update(updatedPoint);
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