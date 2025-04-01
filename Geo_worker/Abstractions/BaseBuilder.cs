using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Geo_worker.Models;

namespace Geo_worker.Abstractions
{
    public abstract class BaseBuilder
    {
        public Layer Layer { get; private protected set; } = new Layer();
        public Action<UIElement, MouseButtonEventArgs> OnShapeClicked { get; set; }
        public abstract void CreateLayer();
        public abstract void BuildPolygon(List<PointDraw> points, int radius, int thickness, Brush? colorFill, Brush? colorOutline, string tag, double opacity);
        public abstract void BuildLine(PointDraw pointBegin, PointDraw pointEnd, int thickness, Brush? colorOutline, string tag, double opacity);
        public abstract void BuildEllipse(PointDraw pointDraw, int radiusHorizontal, int radiusVertical, int thickness, Brush? colorFill, Brush? colorOutline, string tag, double opacity);
        public abstract void BuildRectangle(PointDraw pointDraw, int width, int height, int thickness, Brush? colorFill, Brush? colorOutline, string tag, double opacity);
        public abstract void BuildZigZag(List<PointDraw> points, int thickness, Brush? colorOutline, string tag, double opacity);
        public abstract void BuildText(string text, PointDraw pointDraw, Brush? foreground, string tag, double fontSize, double opacity);
        public abstract Layer GetResult();
        public abstract void ClearLayer();
    }
}
