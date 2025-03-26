using System;
using System.Collections.Generic;
using DataAccessLayer.Abstractions;

namespace DataAccessLayer.Entities;

public partial class CustomerEntity : BaseEntity
{
    public int IdCustomer { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Login { get; set; } = null!;

    public int? IdType { get; set; }

    public virtual CustomerTypeEntity? IdTypeNavigation { get; set; }

    public virtual ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
}
