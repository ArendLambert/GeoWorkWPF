using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Position
{
    public int IdPosition { get; set; }

    public string? Name { get; set; }

    public int? Salary { get; set; }

    public int? IdAccessLevel { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual AccessLevel? IdAccessLevelNavigation { get; set; }
}
