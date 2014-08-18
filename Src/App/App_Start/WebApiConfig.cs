using System.Configuration;
using System.Web.Http;
using System.Web.Http.Validation;
using App.Validation;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;
using RFS.Incident.Api.App_Start;
using RFS.Incident.Api.Repositories;

namespace App
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {


            config.MessageHandlers.Add(new JsonWebTokenValidationHandler()
            {
                Audience = ConfigurationSettings.AppSettings["Auth0ClientId"],  // client id
                SymmetricKey = ConfigurationSettings.AppSettings["Auth0Secret"]   // client secret
            });

            ConfigureContainer(config);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            //config.EnableSystemDiagnosticsTracing();

            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Remove prefixes from ModelState keys
            config.Services.Replace(typeof(IBodyModelValidator), new CustomBodyModelValidator(new DefaultBodyModelValidator()));
        }

        private static void ConfigureContainer(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IIncidentRepository, IncidentRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}