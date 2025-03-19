using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class CustomerType
{
    public int IdCustomerType { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
