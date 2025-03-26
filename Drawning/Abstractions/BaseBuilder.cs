using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drawning.Models;

namespace Drawning.Abstractions
{
    public abstract class BaseBuilder
    {
        public abstract void BuildPoint();
        public abstract void BuildLine();
        public abstract void BuildEllipse();
        public abstract Layer GetResult();
    }
}
