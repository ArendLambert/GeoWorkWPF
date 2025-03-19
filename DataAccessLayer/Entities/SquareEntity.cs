using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class SquareEntity
{
    public int IdSquare { get; set; }

    public string? Name { get; set; }

    public int? Alitude { get; set; }

    public int? IdProject { get; set; }

    public virtual ICollection<AreaCoordinateEntity> AreaCoordinates { get; set; } = new List<AreaCoordinateEntity>();

    public virtual ProjectEntity? IdProjectNavigation { get; set; }

    public virtual ICollection<ProfileEntity> Profiles { get; set; } = new List<ProfileEntity>();
}
