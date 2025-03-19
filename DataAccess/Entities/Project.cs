using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Project
{
    public int IdProject { get; set; }

    public string? Name { get; set; }

    public int? IdCustomer { get; set; }

    public int? IdEmployee { get; set; }

    public virtual Customer? IdCustomerNavigation { get; set; }

    public virtual Employee? IdEmployeeNavigation { get; set; }

    public virtual ICollection<Square> Squares { get; set; } = new List<Square>();
}
