using System.Web.Http;
using RFS.Incident.Api.Models;

namespace RFS.Incident.Api.Controllers
{
    public class LookupController : ApiController
    {
        // GET api/<controller>
        public LookUpModel Get()
        {
            return new LookUpModel();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}