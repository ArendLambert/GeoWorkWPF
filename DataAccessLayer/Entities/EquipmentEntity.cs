using System;
using System.Collections.Generic;
using DataAccessLayer.Abstractions;

namespace DataAccessLayer.Entities;

public partial class EquipmentEntity : BaseEntity
{
    public int IdEquipment { get; set; }

    public string Name { get; set; } = null!;

    public string SerialNumber { get; set; } = null!;

    public virtual ICollection<PointEntity> Points { get; set; } = new List<PointEntity>();
}
