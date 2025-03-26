﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions
{
    public abstract class BaseModel
    {
        public override string ToString()
        {
            var properties = GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var propertyValues = properties.Select(p => $"{p.Name}: {p.GetValue(this)}");
            return string.Join(", ", propertyValues);
        }
    }
}
