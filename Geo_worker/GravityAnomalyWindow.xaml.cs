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
        public PlotModel PlotModel { get; private set; } = new PlotModel();
        public string PicketName { get; private set; }
        private Picket _picket;
        private UnitOfWork _unitOfWork;

        public GravityAnomalyWindow(UnitOfWork unitOfWork, Picket picket)
        {
            InitializeComponent();
            DataContext = this;

            PicketName = picket.Name;
            _picket = picket;
            _unitOfWork = unitOfWork;
            SetupPlot(unitOfWork, picket.Id);
            //PlotModel = new PlotModel { Title = "Аномалия гравитации" };
        }

        private async void SetupPlot(UnitOfWork unitOfWork, int picketId, bool anomaly = true)
        {
            if (anomaly)
            {
                PlotModel.Title = "Аномалия гравитации";
            }
            else
            {
                PlotModel.Title = "Высота";
            }

            PlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Точка (индекс)",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            });
            if (anomaly)
            {
                PlotModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    Title = "Аномалия гравитации (мГал)",
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    Minimum = -2,
                    Maximum = 2
                });
            }
            else
            {
                PlotModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    Title = "Высота (м)",
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    Minimum = 0,
                    Maximum = 150
                });
            }

            var points = await unitOfWork.PointRepository.GetAll();
            points = points.Where(p => p.IdPicket == picketId)
                .OrderBy(p => p.Id)
                .ToList();

            if (anomaly)
            {
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
            else
            {
                var amendents = new LineSeries
                {
                    Title = "Высота",
                    MarkerType = MarkerType.Circle,
                    MarkerSize = 4,
                    Color = OxyColor.FromRgb(0, 255, 0)
                };
                for (int i = 0; i < points.Count; i++)
                {
                    amendents.Points.Add(new DataPoint(i, points[i].Amendments));
                }
                PlotModel.Series.Add(amendents);
            }

            PlotModel.PlotView.InvalidatePlot(true);

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Amendents_Click(object sender, RoutedEventArgs e)
        {
            PlotModel.Series.Clear();
            PlotModel.Axes.Clear();
            SetupPlot(_unitOfWork, _picket.Id, false);
            PlotModel.InvalidatePlot(true);
        }

        private void Anomaly_Click(object sender, RoutedEventArgs e)
        {
            PlotModel.Series.Clear();
            PlotModel.Axes.Clear();

            SetupPlot(_unitOfWork, _picket.Id);
            PlotModel.InvalidatePlot(true);
        }
    }
}