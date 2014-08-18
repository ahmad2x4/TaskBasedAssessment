using System.Collections.Generic;
using MongoDB.Bson;
using RFS.Incident.Api.Models;

namespace RFS.Incident.Api.Repositories
{
    public interface IIncidentRepository
    {
        IncidentModel GetAll();
        Models.Incident Get(BsonObjectId id);
        Models.Incident Add(Models.Incident incident);
        void Remove(BsonObjectId id);
        bool Update(Models.Incident incident);
    }
}
