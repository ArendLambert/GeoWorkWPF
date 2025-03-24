using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class PicketCoordinate
{
    public int IdRecord { get; set; }

    public int? IdPicket { get; set; }

    public double? X { get; set; }

    public double? Y { get; set; }

    public virtual Picket? IdPicketNavigation { get; set; }
}
