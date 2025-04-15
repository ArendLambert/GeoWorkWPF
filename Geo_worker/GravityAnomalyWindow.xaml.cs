using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Core.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Geo_worker
{
    public partial class GravityAnomalyWindow : Window
    {
        public PlotModel PlotModel { get; private set; }
        public string PicketName { get; private set; }

        public GravityAnomalyWindow(UnitOfWork unitOfWork, Picket picket)
        {
            InitializeComponent();
            DataContext = this;

            PicketName = picket.Name;
            SetupPlot(unitOfWork, picket.Id);
        }

        private async void SetupPlot(UnitOfWork unitOfWork, int picketId)
        {
            PlotModel = new PlotModel { Title = "Аномалия гравитации" };

            // Оси
            PlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Точка (индекс)",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            });
            PlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Аномалия гравитации (мГал)",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Minimum = -2,
                Maximum = 2
            });


            var points = await unitOfWork.PointRepository.GetAll();
            points = points.Where(p => p.IdPicket == picketId)
                .OrderBy(p => p.Id)
                .ToList();

            // Создание серии данных
            var series = new LineSeries
            {
                Title = "Аномалия гравитации",
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                Color = OxyColor.FromRgb(0, 0, 255)
            };

            for (int i = 0; i < points.Count; i++)
            {
                series.Points.Add(new DataPoint(i, points[i].GravityAnomaly));
            }

            PlotModel.Series.Add(series);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}