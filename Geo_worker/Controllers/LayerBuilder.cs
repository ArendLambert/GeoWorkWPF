using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo_worker.Abstractions;
using Geo_worker.Models;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.IO;

namespace Geo_worker.Controllers
{
    public class LayerBuilder : BaseBuilder
    {
        public override void CreateLayer()
        {
            Layer = new Layer();
        }
        public override void BuildEllipse(PointDraw pointDraw, int radiusHorizontal, int radiusVertical, int thickness, Brush? colorFill, Brush? colorOutline)
        {
            Ellipse point = new Ellipse
            {
                Width = radiusHorizontal,
                Height = radiusVertical,
                Fill = colorFill,
                Stroke = colorOutline,
                StrokeThickness = thickness
            };
            Canvas.SetLeft(point, pointDraw.X); // Координата X
            Canvas.SetTop(point, pointDraw.Y);  // Координата Y
            Layer.AddPart(point);
        }

        public override void BuildLine(PointDraw pointBegin, PointDraw pointEnd, int thickness, Brush? colorFill, Brush? colorOutline)
        {
            Line line = new Line
            {
                X1 = pointBegin.X, // Начальная точка X
                Y1 = pointBegin.Y, // Начальная точка Y
                X2 = pointEnd.X, // Конечная точка X
                Y2 = pointEnd.Y, // Конечная точка Y
                Stroke = colorOutline,
                Fill = colorFill,
                StrokeThickness = thickness
            };
            Layer.AddPart(line);
        }

        public override void BuildPolygon(List<PointDraw> points, int radius, int thickness, Brush? colorFill, Brush? colorOutline)
        {
            Polygon polygon = new Polygon
            {
                Points = new PointCollection((IEnumerable<Point>)points),
                Fill = colorFill,
                Stroke = colorOutline,
                StrokeThickness = thickness
            };
            Layer.AddPart(polygon);
        }

        public override void BuildRectangle(PointDraw pointDraw, int width, int height, int thickness, Brush? colorFill, Brush? colorOutline)
        {
            Rectangle rectangle = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = colorFill,
                Stroke = colorOutline,
                StrokeThickness = thickness
            };
            Canvas.SetLeft(rectangle, pointDraw.X);
            Canvas.SetTop(rectangle, pointDraw.Y);
            Layer.AddPart(rectangle);
        }

        public override void BuildZigZag(List<PointDraw> points, int thickness, Brush? colorFill, Brush? colorOutline)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                BuildLine(points[i], points[i + 1], thickness, colorFill, colorOutline);
            }
        }

        public override void BuildText(string text, PointDraw pointDraw, Brush? foreground, double fontSize = 12)
        {
            Label label = new Label
            {
                Content = text,
                Foreground = foreground ?? Brushes.Black,
                FontSize = fontSize,
            };
            Canvas.SetLeft(label, pointDraw.X);
            Canvas.SetTop(label, pointDraw.Y);
            Layer.AddPart(label);
        }

        public override Layer GetResult()
        {
            return Layer;
        }

        public override void ClearLayer()
        {
            Layer.Clear();
        }
    }
}
