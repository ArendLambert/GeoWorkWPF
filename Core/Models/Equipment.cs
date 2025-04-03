using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;

namespace Core.Models
{
    public class Equipment : BaseModel
    {

        public string Name { get; protected set; }

        public string SerialNumber { get; protected set; }

        protected Equipment(int idEquipment, string name, string serialNumber)
        {
            Id = idEquipment; Name = name; SerialNumber = serialNumber;
        }

        public static Equipment Create(int idEquipment, string name, string serialNumber)
        {
            return new Equipment(idEquipment, name, serialNumber);
        }
    }
}
