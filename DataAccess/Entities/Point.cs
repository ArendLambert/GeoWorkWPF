using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Point
{
    public int IdPoint { get; set; }

    public double? X { get; set; }

    public double? Y { get; set; }

    public double? Gravity { get; set; }

    public double? GravityAnomaly { get; set; }

    public double? Amendments { get; set; }

    public DateTime? Datetime { get; set; }

    public int? IdOperator { get; set; }

    public int? IdEquipment { get; set; }

    public int? IdPicket { get; set; }

    public virtual Equipment? IdEquipmentNavigation { get; set; }

    public virtual Employee? IdOperatorNavigation { get; set; }

    public virtual Picket? IdPicketNavigation { get; set; }
}
