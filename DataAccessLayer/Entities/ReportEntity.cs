using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class ReportEntity
{
    public int IdReport { get; set; }

    public int? IdEmployee { get; set; }

    public int? IdProject { get; set; }

    public string ReportFile { get; set; } = null!;

    public virtual EmployeeEntity? IdEmployeeNavigation { get; set; }

    public virtual ProjectEntity? IdProjectNavigation { get; set; }
}
