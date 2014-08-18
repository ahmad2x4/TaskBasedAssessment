using System;
using System.ComponentModel;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RFS.Incident.Api.Models
{
    //[JsonConverter(typeof(Newtonsoft.Json.Converters.CustomCreationConverter<Incident>))]
    public class Incident
    {
        public BsonObjectId id { get; set; }
        public string sId { get; set; }
        public string Name { get; set; }
        public AlertLevel AlertLevel { get; set; }
        public string Location { get; set; }
        public string CouncilArea { get; set; }
        public Status Status { get; set; }
        public IncidentType Type { get; set; }
        public string Size { get; set; }
        public string Agency { get; set; }
        public DateTime Updated { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AlertLevel
    {
        [Description("Very High")]
        VeryHigh,
        High,
        Medium,
        Low
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        [Description("Under Control")] 
        UnderControl,
        [Description("Being Controlled")]
        BeingControlled,
        [Description("Out Of Control")]
        OutOfControl
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum IncidentType
    {
        BushFire,
        HazardReduction,
        GrassFire
    }
}