using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo_worker.Models
{
    public class PointDraw
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public PointDraw(int x, int y)
        {
            X = x;
            Y = y;
        }

        public PointDraw Create(int x, int y)
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
    }
}
