using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Profile
{
    public int IdProfile { get; set; }

    public string? Name { get; set; }

    public int? IdSquare { get; set; }

    public virtual Square? IdSquareNavigation { get; set; }

    public virtual ICollection<Picket> Pickets { get; set; } = new List<Picket>();

    public virtual ICollection<ProfileCoordinate> ProfileCoordinates { get; set; } = new List<ProfileCoordinate>();
}
