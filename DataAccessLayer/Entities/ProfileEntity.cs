using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class ProfileEntity
{
    public int IdProfile { get; set; }

    public string Name { get; set; } = null!;

    public int? IdSquare { get; set; }

    public virtual SquareEntity? IdSquareNavigation { get; set; }

    public virtual ICollection<PicketEntity> Pickets { get; set; } = new List<PicketEntity>();

    public virtual ICollection<ProfileCoordinateEntity> ProfileCoordinates { get; set; } = new List<ProfileCoordinateEntity>();
}
