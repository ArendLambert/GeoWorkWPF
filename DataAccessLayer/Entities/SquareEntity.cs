using System;
using System.Collections.Generic;
using DataAccessLayer.Abstractions;

namespace DataAccessLayer.Entities;

public partial class SquareEntity : BaseEntity
{
    public int IdSquare { get; set; }

    public string Name { get; set; } = null!;

    public int Alitude { get; set; }

    public int? IdProject { get; set; }

    public virtual ICollection<AreaCoordinateEntity> AreaCoordinates { get; set; } = new List<AreaCoordinateEntity>();

    public virtual ProjectEntity? IdProjectNavigation { get; set; }

    public virtual ICollection<ProfileEntity> Profiles { get; set; } = new List<ProfileEntity>();
}
