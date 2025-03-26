using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;

namespace Core.Models
{
    public class AuditLogs : BaseModel
    {
        public int Id { get; private set; }
        public DateTime ChangeDateTime { get; private set; } = DateTime.Now;
        public string OldValue { get; private set; } = String.Empty;
        public string NewValue { get; private set; } = String.Empty;
        public string ChangeLocation { get; private set; } = String.Empty;
        public string Username { get; private set; } = String.Empty;
        public string Action { get; private set; } = String.Empty;
        private AuditLogs(int id, DateTime changeDateTime, string oldValue, string newValue, string changeLocation, string username, string action)
        {
            Id = id;
            ChangeDateTime = changeDateTime;
            OldValue = oldValue;
            NewValue = newValue;
            ChangeLocation = changeLocation;
            Username = username;
            Action = action;
        }

        public static AuditLogs Create(int id, DateTime changeDateTime, string oldValue, string newValue, string changeLocation, string username, string action)
        {
            return new AuditLogs(id, changeDateTime, oldValue, newValue, changeLocation, username, action);
        }
    }
}
