using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class AccessLevelEntity
{
    public int IdAccessLevel { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<PositionEntity> Positions { get; set; } = new List<PositionEntity>();
}
