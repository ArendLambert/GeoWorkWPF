using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AreaCoordinate
    {
        public int Id { get; private set; }

        public int? IdSquare { get; private set; }

        public double? X { get; private set; }

        public double? Y { get; private set; }

        private AreaCoordinate(int id, int? idSquare, double? x, double? y)
        {
            Id = id;
            IdSquare = idSquare;
            X = x;
            Y = y;
        }

        public static AreaCoordinate Create(int id, int? idSquare, double? x, double? y)
        {
            return new AreaCoordinate(id, idSquare, x, y);
        }
    }
}
