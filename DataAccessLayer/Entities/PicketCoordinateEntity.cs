using System;
using System.Collections.Generic;
using DataAccessLayer.Abstractions;

namespace DataAccessLayer.Entities;

public partial class PicketCoordinateEntity : BaseEntity
{
    public int IdRecord { get; set; }

    public int? IdPicket { get; set; }

    public double X { get; set; }

    public double Y { get; set; }

    public virtual PicketEntity? IdPicketNavigation { get; set; }
}
