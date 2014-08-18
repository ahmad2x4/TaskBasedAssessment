using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using RFS.Incident.Api.Repositories;
using MongoDB.Bson;
using RFS.Incident.Api.Models;

namespace RFS.Incident.Api.Controllers
{
    public class IncidentController : ApiController
    {
        IIncidentRepository _repository;

        public IncidentController(IIncidentRepository repository)
        {
            _repository = repository;
        }

        // GET api/<controller>
        public IncidentModel Get()
        {
            return _repository.GetAll();
        }

        // GET api/<controller>/5
        public Models.Incident Get(string id)        
        {
            return _repository.Get(ObjectId.Parse(id));
        }

        // POST api/<controller>
        [Authorize]
        public bool Post(Models.Incident incident)
        {
            incident.Updated = DateTime.Now;
            _repository.Add(incident);
            return true;
        }

        // PUT api/<controller>/5
        [Authorize]
        public bool Put(Models.Incident incident)
        {
            incident.Updated = DateTime.Now;
            _repository.Update(incident);
            return true;
        }

        // DELETE api/<controller>/5
        [Authorize]
        public void Delete(string id)   
        {
            _repository.Remove(ObjectId.Parse(id));
        }       
    }
}