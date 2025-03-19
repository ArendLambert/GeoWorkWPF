using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Entities;

public partial class AccessLevelEntity
{
    public int IdAccessLevel { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<PositionEntity> Positions { get; set; } = new List<PositionEntity>();
}
