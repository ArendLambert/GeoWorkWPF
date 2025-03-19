using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class EmployeeEntity
{
    public int IdEmployee { get; set; }

    public string? Passport { get; set; }

    public int? IdPosition { get; set; }

    public virtual PositionEntity? IdPositionNavigation { get; set; }

    public virtual ICollection<PointEntity> Points { get; set; } = new List<PointEntity>();

    public virtual ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
}
