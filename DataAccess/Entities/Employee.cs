using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public string? Passport { get; set; }

    public int? IdPosition { get; set; }

    public virtual Position? IdPositionNavigation { get; set; }

    public virtual ICollection<Point> Points { get; set; } = new List<Point>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
