using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class CustomerTypeEntity
{
    public int IdCustomerType { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<CustomerEntity> Customers { get; set; } = new List<CustomerEntity>();
}
