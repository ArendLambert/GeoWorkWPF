using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class PointEntity
{
    public int IdPoint { get; set; }

    public double X { get; set; }

    public double Y { get; set; }

    public double Gravity { get; set; }

    public double GravityAnomaly { get; set; }

    public double Amendments { get; set; }

    public DateTime Datetime { get; set; }

    public int? IdOperator { get; set; }

    public int? IdEquipment { get; set; }

    public int? IdPicket { get; set; }

    public virtual EquipmentEntity? IdEquipmentNavigation { get; set; }

    public virtual EmployeeEntity? IdOperatorNavigation { get; set; }

    public virtual PicketEntity? IdPicketNavigation { get; set; }
}
