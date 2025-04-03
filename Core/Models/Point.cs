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

        public double X { get; protected set; }

        public double Y { get; protected set; }

        public double Gravity { get; protected set; }

        public double GravityAnomaly { get; protected set; }

        public double Amendments { get; protected set; }

        public DateTime Datetime { get; protected set; }

        public int? IdOperator { get; protected set; }

        public int? IdEquipment { get; protected set; }

        public int? IdPicket { get; protected set; }

        protected Point(int idPoint, double x, double y, double gravity, double gravityAnomaly, double amendments, DateTime datetime, int? idOperator, int? idEquipment, int? idPicket)
        {
            Id = idPoint; X = x; Y = y; Gravity = gravity; GravityAnomaly = gravityAnomaly; Amendments = amendments; Datetime = datetime; IdOperator = idOperator; IdEquipment = idEquipment; IdPicket = idPicket;
        }

        public static Point Create(int idPoint, double x, double y, double gravity, double gravityAnomaly, double amendments, DateTime datetime, int? idOperator, int? idEquipment, int? idPicket)
        {
            return new Point(idPoint, x, y, gravity, gravityAnomaly, amendments, datetime, idOperator, idEquipment, idPicket);
        }
    }
}
