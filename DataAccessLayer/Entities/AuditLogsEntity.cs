using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstractions;

namespace DataAccessLayer.Entities
{
    public class AuditLogsEntity : BaseEntity
    {
        public int Id { get; set; }
        public DateTime ChangeDateTime { get; set; }
        public string OldValue { get; set; } = null!;
        public string NewValue { get; set; } = null!;
        public string ChangeLocation { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Action { get; set; } = null!;
    }
}
