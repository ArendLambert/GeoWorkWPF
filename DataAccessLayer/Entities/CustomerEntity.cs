using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class CustomerEntity
{
    public int IdCustomer { get; set; }

    public int? IdType { get; set; }

    public virtual CustomerTypeEntity? IdTypeNavigation { get; set; }

    public virtual ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
}
