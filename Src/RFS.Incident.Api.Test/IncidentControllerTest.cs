using System.Linq;
using RFS.Incident.Api.Controllers;
using Xunit;

namespace RFS.Incident.Api.Test
{
    public class IncidentControllerTest
    {
        private readonly IncidentRepositoryStub _incidentRepository = new IncidentRepositoryStub();

        [Fact]
        public void GetAllShouldReturnAllIncidents()
        {
            var controller = new IncidentController(_incidentRepository);
            var result = controller.Get();

            Assert.Equal(_incidentRepository.GetAll().Incidents, result.Incidents);
        }

        [Fact]
        public void GetAllShouldReturnById()
        {
            var controller = new IncidentController(_incidentRepository);
            var result = controller.Get(_incidentRepository.GetAll().Incidents.First().id.ToString());

            Assert.Equal(_incidentRepository.GetAll().Incidents.First().id, result.id);
        }

        [Fact]
        public void PostShouldAddNewIncident()
        {
            var incident = new Models.Incident();
            var count = _incidentRepository.GetAll().Incidents.Count;

            var controller = new IncidentController(_incidentRepository);
            var result = controller.Post(incident);

            Assert.Equal(count + 1, _incidentRepository.GetAll().Incidents.Count);
        }

        [Fact]
        public void DeleteShouldAddRemoveIncident()
        {
            var count = _incidentRepository.GetAll().Incidents.Count;

            var controller = new IncidentController(_incidentRepository);
            controller.Delete(_incidentRepository.GetAll().Incidents.First().id.ToString());

            Assert.Equal(count - 1, _incidentRepository.GetAll().Incidents.Count);
        }

    }
}
