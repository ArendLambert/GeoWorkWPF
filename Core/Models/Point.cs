using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;

namespace Core.Models
{
    public class Point : BaseModel
    {
        public int Id { get; private set; }

        public double X { get; private set; }

        public double Y { get; private set; }

        public double Gravity { get; private set; }

        public double GravityAnomaly { get; private set; }

        public double Amendments { get; private set; }

        public DateTime Datetime { get; private set; }

        public int? IdOperator { get; private set; }

        public int? IdEquipment { get; private set; }

        public int? IdPicket { get; private set; }

        private Point(int idPoint, double x, double y, double gravity, double gravityAnomaly, double amendments, DateTime datetime, int? idOperator, int? idEquipment, int? idPicket)
        {
            Id = idPoint; X = x; Y = y; Gravity = gravity; GravityAnomaly = gravityAnomaly; Amendments = amendments; Datetime = datetime; IdOperator = idOperator; IdEquipment = idEquipment; IdPicket = idPicket;
        }

        public static Point Create(int idPoint, double x, double y, double gravity, double gravityAnomaly, double amendments, DateTime datetime, int? idOperator, int? idEquipment, int? idPicket)
        {
            return new Point(idPoint, x, y, gravity, gravityAnomaly, amendments, datetime, idOperator, idEquipment, idPicket);
        }
    }
}
