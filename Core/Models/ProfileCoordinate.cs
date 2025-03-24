using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ProfileCoordinate
    {
        public int Id { get; private set; }

        public int? IdProfile { get; private set; }

        public double? X { get; private set; }

        public double? Y { get; private set; }

        private ProfileCoordinate(int idRecord, int? idProfile, double? x, double? y)
        {
            Id = idRecord;
            IdProfile = idProfile;
            X = x;
            Y = y;
        }

        public static ProfileCoordinate Create(int idRecord, int? idProfile, double? x, double? y)
        {
            return new ProfileCoordinate(idRecord, idProfile, x, y);
        }
    }
}
