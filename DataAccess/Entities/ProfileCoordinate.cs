using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class ProfileCoordinate
{
    public int IdRecord { get; set; }

    public int? IdProfile { get; set; }

    public double? X { get; set; }

    public double? Y { get; set; }

    public virtual Profile? IdProfileNavigation { get; set; }
}
