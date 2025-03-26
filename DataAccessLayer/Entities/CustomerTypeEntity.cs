using System;
using System.Collections.Generic;
using DataAccessLayer.Abstractions;

namespace DataAccessLayer.Entities;

public partial class CustomerTypeEntity : BaseEntity
{
    public int IdCustomerType { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<CustomerEntity> Customers { get; set; } = new List<CustomerEntity>();
}
