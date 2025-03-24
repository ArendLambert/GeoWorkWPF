using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class ProjectEntity
{
    public int IdProject { get; set; }

    public string Name { get; set; } = null!;

    public int? IdCustomer { get; set; }

    public int? IdEmployee { get; set; }

    public virtual CustomerEntity? IdCustomerNavigation { get; set; }

    public virtual EmployeeEntity? IdEmployeeNavigation { get; set; }

    public virtual ICollection<ReportEntity> Reports { get; set; } = new List<ReportEntity>();

    public virtual ICollection<SquareEntity> Squares { get; set; } = new List<SquareEntity>();
}
