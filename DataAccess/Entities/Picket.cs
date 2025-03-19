using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Picket
{
    public int IdPicket { get; set; }

    public string? Name { get; set; }

    public int? IdProfile { get; set; }

    public virtual Profile? IdProfileNavigation { get; set; }

    public virtual ICollection<PicketCoordinate> PicketCoordinates { get; set; } = new List<PicketCoordinate>();

    public virtual ICollection<Point> Points { get; set; } = new List<Point>();
}
