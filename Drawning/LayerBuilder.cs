using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drawning.Abstractions;
using Drawning.Models;
using System.Windows.Controls;

namespace Drawning
{
    public class LayerBuilder : BaseBuilder
    {
        private Layer _layer = new Layer();
        public override void BuildEllipse(Canvas canvas)
        {
            // Рисуем точку (маленький круг)
            Ellipse point = new Ellipse
            {
                Width = 5,
                Height = 5,
                Fill = Brushes.Green
            };
            Canvas.SetLeft(point, 50); // Координата X
            Canvas.SetTop(point, 50);  // Координата Y
            DrawingCanvas.Children.Add(point);
        }

        public override void BuildLine()
        {
            throw new NotImplementedException();
        }

        public override void BuildPoint()
        {
            throw new NotImplementedException();
        }

        public override Layer GetResult()
        {
            return _layer;
        }
    }
}
