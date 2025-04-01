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
using System.Windows.Input;

namespace Geo_worker.Controllers
{
    public class LayerBuilder : BaseBuilder
    {
        public override void CreateLayer()
        {
            Layer = new Layer();
        }
        public override void BuildEllipse(PointDraw pointDraw, int radiusHorizontal, int radiusVertical, int thickness, Brush? colorFill, Brush? colorOutline, string tag, double opacity = 1.0)
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
            point.Tag = tag;
            if (OnShapeClicked != null)
            {
                point.MouseLeftButtonUp += (s, e) => OnShapeClicked(point, e);
            }
            Layer.AddPart(point);
        }

        public override void BuildLine(PointDraw pointBegin, PointDraw pointEnd, int thickness, Brush? colorOutline, string tag, double opacity = 1.0)
        {
            Line line = new Line
            {
                X1 = pointBegin.X, // Начальная точка X
                Y1 = pointBegin.Y, // Начальная точка Y
                X2 = pointEnd.X, // Конечная точка X
                Y2 = pointEnd.Y, // Конечная точка Y
                Stroke = colorOutline,
                Fill = null,
                StrokeThickness = thickness
            };
            line.Tag = tag;
            if (OnShapeClicked != null)
            {
                line.MouseLeftButtonUp += (s, e) => OnShapeClicked(line, e);
            }
            Layer.AddPart(line);
        }

        public override void BuildPolygon(List<PointDraw> points, int radius, int thickness, Brush? colorFill, Brush? colorOutline, string tag, double opacity = 1.0)
        {
            Polygon polygon = new Polygon
            {
                Points = new PointCollection(points.Select(p => new System.Windows.Point(p.X, p.Y))),
                Fill = colorFill,
                Stroke = colorOutline,
                StrokeThickness = thickness
            };
            polygon.Tag = tag;
            if (OnShapeClicked != null)
            {
                polygon.MouseLeftButtonUp += (s, e) => OnShapeClicked(polygon, e);
            }
            Layer.AddPart(polygon);
        }

        public override void BuildRectangle(PointDraw pointDraw, int width, int height, int thickness, Brush? colorFill, Brush? colorOutline, string tag, double opacity = 1.0)
        {
            Rectangle rectangle = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = colorFill,
                Stroke = colorOutline,
                StrokeThickness = thickness,
                Opacity = opacity
            };
            Canvas.SetLeft(rectangle, pointDraw.X);
            Canvas.SetTop(rectangle, pointDraw.Y);
            rectangle.Tag = tag;
            if (OnShapeClicked != null)
            {
                rectangle.MouseLeftButtonUp += (s, e) => OnShapeClicked(rectangle, e);
            }
            Layer.AddPart(rectangle);
        }

        public override void BuildZigZag(List<PointDraw> points, int thickness, Brush? colorOutline, string tag, double opacity = 1.0)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                BuildLine(points[i], points[i + 1], thickness, colorOutline, tag, opacity);
            }
        }

        public override void BuildText(string text, PointDraw pointDraw, Brush? foreground, string tag, double fontSize = 12, double opacity = 1.0)
        {
            Label label = new Label
            {
                Content = text,
                Foreground = foreground ?? Brushes.Black,
                FontSize = fontSize,
            };
            Canvas.SetLeft(label, pointDraw.X);
            Canvas.SetTop(label, pointDraw.Y);
            label.Tag = tag;
            if (OnShapeClicked != null)
            {
                label.MouseLeftButtonUp += (s, e) => OnShapeClicked(label, e);
            }
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
