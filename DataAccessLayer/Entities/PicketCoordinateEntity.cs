using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class PicketCoordinateEntity
{
    public int IdRecord { get; set; }

    public int? IdPicket { get; set; }

    public double X { get; set; }

    public double Y { get; set; }

    public virtual PicketEntity? IdPicketNavigation { get; set; }
}
