﻿using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class PositionEntity
{
    public int IdPosition { get; set; }

    public string? Name { get; set; } = null!;

    public int? Salary { get; set; }

    public int? IdAccessLevel { get; set; }

    public virtual ICollection<EmployeeEntity> Employees { get; set; } = new List<EmployeeEntity>();

    public virtual AccessLevelEntity? IdAccessLevelNavigation { get; set; }
}
