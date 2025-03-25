using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class ProfileCoordinateEntity
{
    public int IdRecord { get; set; }

    public int? IdProfile { get; set; }

    public double X { get; set; }

    public double Y { get; set; }

    public virtual ProfileEntity? IdProfileNavigation { get; set; }
}
