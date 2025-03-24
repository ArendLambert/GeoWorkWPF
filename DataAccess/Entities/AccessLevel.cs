using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class AccessLevel
{
    public int IdAccessLevel { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
}
