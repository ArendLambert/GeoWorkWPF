using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Geo_worker.Models
{
    public class PointDraw
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public PointDraw(double x, double y)
        {
            X = x;
            Y = y;
        }

        public PointDraw Create(double x, double y)
        {
            return new PointDraw(x, y);
        }

        public void ChangeX(int x)
        {
            X = x;
        }

        public void ChangeY(int y)
        {
            Y = y;
        }

        public static PointDraw CreatePointDrawFromGeoPoint(Point point)
        {
            return new PointDraw(point.X, point.Y);
        }
    }
}
