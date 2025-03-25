using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class EmployeeEntity
{
    public int IdEmployee { get; set; }

    public string Passport { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Login { get; set; } = null!;

    public int? IdPosition { get; set; }

    public virtual PositionEntity? IdPositionNavigation { get; set; }

    public virtual ICollection<PointEntity> Points { get; set; } = new List<PointEntity>();

    public virtual ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();

    public virtual ICollection<ReportEntity> Reports { get; set; } = new List<ReportEntity>();
}
