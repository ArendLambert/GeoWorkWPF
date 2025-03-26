using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Geo_worker.Models
{
    public class Layer
    {
        public List<object> Parts { get; private set; } = new List<object>();
        public Layer() { }

        public void AddPart(object part)
        {
            Parts.Add(part);
        }

        public void RemovePart(object part)
        {
            Parts.Remove(part);
        }

        public void Clear()
        {
            Parts.Clear();
        }
    }
}
