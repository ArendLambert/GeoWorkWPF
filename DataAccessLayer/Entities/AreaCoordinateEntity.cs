using System;
using System.Collections.Generic;
using DataAccessLayer.Abstractions;

namespace DataAccessLayer.Entities;

public partial class AreaCoordinateEntity : BaseEntity
{
    public int IdRecord { get; set; }

    public int? IdSquare { get; set; }

    public double X { get; set; }

    public double Y { get; set; }

    public virtual SquareEntity? IdSquareNavigation { get; set; }
}
