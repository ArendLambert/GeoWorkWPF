using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;

namespace Core.Models
{
    public class Report : BaseModel
    {

        public int? IdEmployee { get; private set; }

        public int? IdProject { get; private set; }

        public string ReportFile { get; private set; } = null!;

        private Report(int id, int? idEmployee, int? idProject, string reportFile)
        {
            Id = id;
            IdEmployee = idEmployee;
            IdProject = idProject;
            ReportFile = reportFile;
        }
        
        public static Report Create(int id, int? idEmployee, int? idProject, string reportFile)
        {
            return new Report(id, idEmployee, idProject, reportFile);
        }
    }
}
