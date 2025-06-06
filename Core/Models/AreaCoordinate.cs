﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;

namespace Core.Models
{
    public class AreaCoordinate : BaseModel
    {

        public int? IdSquare { get; private set; }

        public double X { get; private set; }

        public double Y { get; private set; }

        private AreaCoordinate(int id, int? idSquare, double x, double y)
        {
            Id = id; 
            IdSquare = idSquare; 
            X = x; Y = y;
        }

        public static AreaCoordinate Create(int id, int? idSquare, double x, double y)
        {
            return new AreaCoordinate(id, idSquare, x, y);
        }
    }
}
