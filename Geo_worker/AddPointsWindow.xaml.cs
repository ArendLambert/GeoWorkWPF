using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Core.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace Geo_worker
{
    public partial class AddPointsWindow : Window, INotifyPropertyChanged
    {
        public int UserId { get; private set; } = 0;
        public int ProjectId { get; private set; }
        private Core.Models.Point _point;

        // Коллекции для ComboBox
        private List<MinEmployee> _operators;
        private List<MinEquipment> _equipment;
        private List<MinPicket> _pickets;

        private UnitOfWork _unitOfWork;

        // Выбранные значения
        private int? _selectedOperatorId;
        private int? _selectedEquipmentId;
        private int? _selectedPicketId;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<MinEmployee> Operators
        {
            get => _operators;
            set
            {
                _operators = value;
                OnPropertyChanged(nameof(Operators));
            }
        }

        public List<MinEquipment> Equipment
        {
            get => _equipment;
            set
            {
                _equipment = value;
                OnPropertyChanged(nameof(Equipment));
            }
        }

        public List<MinPicket> Pickets
        {
            get => _pickets;
            set
            {
                _pickets = value;
                OnPropertyChanged(nameof(Pickets));
            }
        }

        public int? SelectedOperatorId
        {
            get => _selectedOperatorId;
            set
            {
                _selectedOperatorId = value;
                OnPropertyChanged(nameof(SelectedOperatorId));
            }
        }

        public int? SelectedEquipmentId
        {
            get => _selectedEquipmentId;
            set
            {
                _selectedEquipmentId = value;
                OnPropertyChanged(nameof(SelectedEquipmentId));
            }
        }

        public int? SelectedPicketId
        {
            get => _selectedPicketId;
            set
            {
                _selectedPicketId = value;
                OnPropertyChanged(nameof(SelectedPicketId));
            }
        }

        public AddPointsWindow(int userId, int projectId, Core.Models.Point point = null)
        {
            InitializeComponent();
            _point = point;
            DataContext = this;
            UserId = userId;
            ProjectId = projectId;
            _unitOfWork = new UnitOfWork(new GravitySurveyOnDeleteNoAction(), UserId);
            // Загрузка данных для ComboBox (предполагается, что у вас есть соответствующие методы)
            LoadComboBoxData();

            if (_point != null)
            {
                // Заполнение полей для редактирования
                txtX.Text = _point.X.ToString();
                txtY.Text = _point.Y.ToString();
                txtGravity.Text = _point.Gravity.ToString();
                txtGravityAnomaly.Text = _point.GravityAnomaly.ToString();
                txtAmendments.Text = _point.Amendments.ToString();
                dpDateTime.SelectedDate = _point.Datetime;
                SelectedOperatorId = _point.IdOperator;
                SelectedEquipmentId = _point.IdEquipment;
                SelectedPicketId = _point.IdPicket;
            }
            else
            {
                dpDateTime.SelectedDate = DateTime.Now;
            }
        }

        private async void LoadComboBoxData()
        {
            // Здесь должна быть ваша логика загрузки данных
            // Это пример, замените на реальную реализацию
            List<Employee> employees = await _unitOfWork.EmployeeRepository.GetAll();
            List<Equipment> equipment = await _unitOfWork.EquipmentRepository.GetAll();
            List<Picket> pickets = await _unitOfWork.PicketRepository.GetAll();
            List<Profile> profiles = await _unitOfWork.ProfileRepository.GetAll();
            List<Square> squares = await _unitOfWork.SquareRepository.GetAll();
            squares = squares.Where(p => p.IdProject == ProjectId).ToList();
            profiles = profiles.Where(p => squares.Any(s => p.IdSquare == s.Id)).ToList();
            pickets = pickets.Where(p => profiles.Any(s => p.IdProfile == s.Id)).ToList();
            Operators = employees.Select(e => new MinEmployee(e.Id, e.Passport)).ToList();
            Equipment = equipment.Select(e => new MinEquipment(e.Id, e.Name)).ToList();
            Pickets = pickets.Select(p => new MinPicket(p.Id, p.Name)).ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Предполагается, что ID генерируется где-то еще при создании новой точки
                int id = _point?.Id ?? 0;
                double x = double.Parse(txtX.Text);
                double y = double.Parse(txtY.Text);
                double gravity = double.Parse(txtGravity.Text);
                double gravityAnomaly = double.Parse(txtGravityAnomaly.Text);
                double amendments = double.Parse(txtAmendments.Text);
                DateTime dateTime = dpDateTime.SelectedDate ?? DateTime.Now;

                _point = Core.Models.Point.Create(id, x, y, gravity, gravityAnomaly, amendments, dateTime,
                    SelectedOperatorId, SelectedEquipmentId, SelectedPicketId);

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public Core.Models.Point GetPoint()
        {
            return _point;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class MinEmployee : Employee
        {
            public MinEmployee(int id, string name) : base(id, name, null, null, null)
            {
                Id = id;
                Passport = name;
            }
            public override string ToString()
            {
                return Passport;
            }
        }

        public class MinEquipment : Equipment
        {
            public MinEquipment(int id, string name) : base(id, name, null)
            {
                Id = id;
                Name = name;
            }
            public override string ToString()
            {
                return Name;
            }
        }

        public class MinPicket : Picket
        {
            public MinPicket(int id, string name) : base(id, name, null)
            {
                Id = id;
                Name = name;
            }
            public override string ToString()
            {
                return Name;
            }
        }
    }

}