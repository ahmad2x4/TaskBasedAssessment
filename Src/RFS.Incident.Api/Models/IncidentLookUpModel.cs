using System;
using System.Collections.Generic;
using System.Linq;

namespace RFS.Incident.Api.Models
{
    public class LookUpModel
    {
        public List<string> AlertLevels = Enum.GetNames(typeof(AlertLevel)).ToList();
        public List<string> Status = Enum.GetNames(typeof(Status)).ToList();
        public List<string> Type = Enum.GetNames(typeof(IncidentType)).ToList();
    }
}