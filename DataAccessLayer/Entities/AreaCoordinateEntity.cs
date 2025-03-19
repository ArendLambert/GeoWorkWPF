using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class AreaCoordinateEntity
{
    public int IdRecord { get; set; }

    public int? IdSquare { get; set; }

    public double? X { get; set; }

    public double? Y { get; set; }

    public virtual SquareEntity? IdSquareNavigation { get; set; }
}
