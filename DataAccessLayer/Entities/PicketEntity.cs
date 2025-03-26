using System;
using System.Collections.Generic;
using DataAccessLayer.Abstractions;

namespace DataAccessLayer.Entities;

public partial class PicketEntity : BaseEntity
{
    public int IdPicket { get; set; }

    public string Name { get; set; } = null!;

    public int? IdProfile { get; set; }

    public virtual ProfileEntity? IdProfileNavigation { get; set; }

    public virtual ICollection<PicketCoordinateEntity> PicketCoordinates { get; set; } = new List<PicketCoordinateEntity>();

    public virtual ICollection<PointEntity> Points { get; set; } = new List<PointEntity>();
}
