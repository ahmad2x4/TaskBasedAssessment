using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using RFS.Incident.Api.Models;
using MongoDB.Driver.Builders;

namespace RFS.Incident.Api.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        readonly MongoDatabase projectDB;

        public IncidentRepository()
        {
            projectDB = getProjectDB();
        }

        public IncidentModel GetAll()
        {
            var modelCollection = projectDB.GetCollection("incidents").FindAll().AsEnumerable();
           
            IncidentModel incidentModel = new IncidentModel();
            incidentModel.Incidents = (from incident in modelCollection
                               select new Models.Incident
                                {
                                    sId = incident["_id"].ToString(),
                                    Name = incident["Name"].ToString(),
                                    AlertLevel = (AlertLevel)Enum.Parse(typeof(AlertLevel), incident["AlertLevel"].ToString()),
                                    Location = incident["Location"].ToString(),
                                    CouncilArea = incident["CouncilArea"].ToString(),
                                    Status = (Status)Enum.Parse(typeof(Status), incident["Status"].ToString()),
                                    Type = (IncidentType)Enum.Parse(typeof(IncidentType), incident["Status"].ToString()),
                                    Size = incident["Size"].ToString(),
                                    Agency = incident["Agency"].ToString(),
                                    Updated = incident["Updated"].ToLocalTime()
                                }).ToList();

            return incidentModel;
        }

        public Models.Incident Get(BsonObjectId id)
        {
            var incident = projectDB.GetCollection("incidents").FindOneById(id);

            if (incident.Count() > 0)
            {
                return new Models.Incident
                {
                    sId = incident["_id"].ToString(),
                    Name = incident["Name"].ToString(),
                    AlertLevel = (AlertLevel)Enum.Parse(typeof(AlertLevel), incident["AlertLevel"].ToString()),
                    Location = incident["Location"].ToString(),
                    CouncilArea = incident["CouncilArea"].ToString(),
                    Status = (Status)Enum.Parse(typeof(Status), incident["Status"].ToString()),
                    Type = (IncidentType)Enum.Parse(typeof(IncidentType), incident["Status"].ToString()),
                    Size = incident["Size"].ToString(),
                    Agency = incident["Agency"].ToString(),
                    Updated = incident["Updated"].ToLocalTime()
                };
            }

            return null;           
        }

        public Models.Incident Add(Models.Incident incident)
        {
            var modelCollection = projectDB.GetCollection("incidents");

            modelCollection.Insert(incident);

            return incident;
        }

        public void Remove(BsonObjectId id)
        {
            var incidents = projectDB.GetCollection("incidents");
           
            var query = Query.In("_id", new BsonArray(new[] { id }));

            incidents.Remove(query);
        }

        public bool Update(Models.Incident incident)
        {
            var incidents = projectDB.GetCollection("incidents");
            var currIncident = incidents.FindOneById(ObjectId.Parse(incident.sId));

            currIncident["Name"] = incident.Name;
            currIncident["AlertLevel"] = incident.AlertLevel;
            currIncident["Location"] = incident.Location;
            currIncident["CouncilArea"] = incident.CouncilArea;
            currIncident["Status"] = incident.Status;
            currIncident["Type"] = incident.Type;
            currIncident["Size"] = incident.Size;
            currIncident["Agency"] = incident.Agency;
            currIncident["Updated"] = incident.Updated;
           
            incidents.Save(currIncident);
            return true;
        }


        static MongoDatabase getProjectDB()
        {
            return MongoServer.Create(
                ConfigurationManager.ConnectionStrings["project"].ConnectionString)
                .GetDatabase("ui-project");
        }
    }
}