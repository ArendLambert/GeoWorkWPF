using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Equipment
{
    public int IdEquipment { get; set; }

    public string? Name { get; set; }

    public string? SerialNumber { get; set; }

    public virtual ICollection<Point> Points { get; set; } = new List<Point>();
}
