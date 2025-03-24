using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Customer
{
    public int IdCustomer { get; set; }

    public int? IdType { get; set; }

    public virtual CustomerType? IdTypeNavigation { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
