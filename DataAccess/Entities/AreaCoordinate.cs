using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class AreaCoordinate
{
    public int IdRecord { get; set; }

    public int? IdSquare { get; set; }

    public double? X { get; set; }

    public double? Y { get; set; }

    public virtual Square? IdSquareNavigation { get; set; }
}
