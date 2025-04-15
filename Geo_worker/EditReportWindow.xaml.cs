using System.Windows;
using Core.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace Geo_worker
{
    public partial class EditReportWindow : Window
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Report _report;

        public EditReportWindow(UnitOfWork unitOfWork, Report report)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _report = report;

            WinLoad().GetAwaiter();

            if (_report != null)
            {
                cbEmployee.SelectedValue = _report.IdEmployee;
                cbProject.SelectedValue = _report.IdProject;
                txtReportFile.Text = _report.ReportFile;
            }
        }

        private async Task WinLoad()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAll();
            cbEmployee.ItemsSource = employees;

            var projects = await _unitOfWork.ProjectRepository.GetAll();
            cbProject.ItemsSource = projects;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReportFile.Text))
            {
                MessageBox.Show("Укажите файл отчета.");
                return;
            }

            if (_report == null)
            {
                var newReport = Report.Create(0, (int?)cbEmployee.SelectedValue, (int?)cbProject.SelectedValue, txtReportFile.Text);
                await _unitOfWork.ReportRepository.Create(newReport);
            }
            else
            {
                var updatedReport = Report.Create(_report.Id, (int?)cbEmployee.SelectedValue, (int?)cbProject.SelectedValue, txtReportFile.Text);
                await _unitOfWork.ReportRepository.Update(updatedReport);
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