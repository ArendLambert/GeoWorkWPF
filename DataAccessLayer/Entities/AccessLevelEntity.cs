using System;
using System.Collections.Generic;
using Core.Abstractions;
using DataAccessLayer.Abstractions;

namespace DataAccessLayer.Entities;

public partial class AccessLevelEntity : BaseEntity
{
    public int IdAccessLevel { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<PositionEntity> Positions { get; set; } = new List<PositionEntity>();
}
