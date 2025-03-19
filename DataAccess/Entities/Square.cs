using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Square
{
    public int IdSquare { get; set; }

    public string? Name { get; set; }

    public int? Alitude { get; set; }

    public int? IdProject { get; set; }

    public virtual ICollection<AreaCoordinate> AreaCoordinates { get; set; } = new List<AreaCoordinate>();

    public virtual Project? IdProjectNavigation { get; set; }

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();
}
