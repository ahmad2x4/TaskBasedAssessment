using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using RFS.Incident.Api.Models;
using RFS.Incident.Api.Repositories;

namespace RFS.Incident.Api.Test
{
    public class IncidentRepositoryStub : IIncidentRepository
    {
        private IncidentModel _incidentModel;

        public IncidentRepositoryStub()
        {
            _incidentModel = new IncidentModel();
            _incidentModel.Incidents = new List<Models.Incident>();
            _incidentModel.Incidents.Add(new Models.Incident()
            {
                id = ObjectId.GenerateNewId(),
                Agency = "agency1",
            });
            _incidentModel.Incidents.Add(new Models.Incident()
            {
                id = ObjectId.GenerateNewId(),
                Agency = "agency2",
            });
        }

        public IncidentModel GetAll()
        {
            return _incidentModel;
        }

        public Models.Incident Get(BsonObjectId id)
        {
            return _incidentModel.Incidents.FirstOrDefault(_ => _.id == id);
        }

        public Models.Incident Add(Models.Incident incident)
        {
            _incidentModel.Incidents.Add(incident);
            return incident;
        }

        public void Remove(BsonObjectId id)
        {
            _incidentModel.Incidents.Remove(Get(id));
        }

        public bool Update(Models.Incident incident)
        {
            var updateIncident = Get(incident.id);
            updateIncident.Agency = incident.Agency;
            updateIncident.AlertLevel = incident.AlertLevel;
            updateIncident.CouncilArea = incident.CouncilArea;
            updateIncident.Location = incident.Location;
            updateIncident.Name = incident.Name;
            updateIncident.Size = incident.Size;
            updateIncident.Status = incident.Status;
            return true;
        }
    }
}