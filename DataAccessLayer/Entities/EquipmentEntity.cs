using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class EquipmentEntity
{
    public int IdEquipment { get; set; }

    public string Name { get; set; } = null!;

    public string SerialNumber { get; set; } = null!;

    public virtual ICollection<PointEntity> Points { get; set; } = new List<PointEntity>();
}
