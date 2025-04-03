using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;

namespace Core.Models
{
    public class PicketCoordinate : BaseModel
    {

        public int? IdPicket { get; private set; }

        public double X { get; private set; }

        public double Y { get; private set; }

        private PicketCoordinate(int id, int? idPicket, double x, double y)
        {
            Id = id; IdPicket = idPicket; X = x; Y = y;
        }

        public static PicketCoordinate Create(int id, int? idPicket, double x, double y)
        {
            return new PicketCoordinate(id, idPicket, x, y);
        }
    }
}
