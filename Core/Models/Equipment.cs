using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Equipment
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string SerialNumber { get; private set; }

        private Equipment(int idEquipment, string name, string serialNumber)
        {
            Id = idEquipment; Name = name; SerialNumber = serialNumber;
        }

        public static Equipment Create(int idEquipment, string name, string serialNumber)
        {
            return new Equipment(idEquipment, name, serialNumber);
        }
    }
}
