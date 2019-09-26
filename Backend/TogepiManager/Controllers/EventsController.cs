using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HEREMaps.Base;
using HEREMaps.LocationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TogepiManager.APIModels.Requests;
using TogepiManager.APIModels.Responses;
using TogepiManager.APIModels.SubModels;
using TogepiManager.Consts;
using TogepiManager.DbManagement;

namespace TogepiManager.Controllers
{
    /// <summary>
    /// A controller that has functions to manage all of the events.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        /// <summary>
        /// The database to use.
        /// </summary>
        private readonly TogepiContext dbContext;

        /// <summary>
        /// The logger to use.
        /// </summary>
        private readonly ILogger<EventsController> logger;

        /// <summary>
        /// The API key to use for HERE Maps.
        /// </summary>
        private readonly HEREApp apiKey;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="context">The database to use</param>
        /// <param name="loggerArg">The logger to use</param>
        public EventsController(TogepiContext context, ILogger<EventsController> loggerArg)
        {
            // Saving the args
            dbContext = context;
            logger = loggerArg;

            // Creating the SQL tables
            dbContext.Database.EnsureCreated();

            // Setting the API key
            apiKey = new HEREApp
            {
                AppId = "nwVKikGG0miA826GlXkr",
                AppCode = "U_lOt-47GHaEnnNs34gJ6w"
            };
        }

        /// <summary>
        /// Deletes all of the events and reports.
        /// </summary>
        /// <response code="200">The response was OK, and we tried to delete all of the events</response>
        [HttpDelete]
        [ProducesResponseType(typeof(AllEventsResponseModel), (int)HttpStatusCode.OK)]
        public IActionResult DeleteEveryEvent()
        {
            // Delete every event
            foreach (var id in dbContext.Events.Select(e => e.Id))
            {
                var entity = new Event { Id = id };
                dbContext.Events.Attach(entity);
                dbContext.Events.Remove(entity);
            }

            // Delete every report
            foreach (var reportId in dbContext.Reports.Select(r => r.Id))
            {
                var entity = new Report { Id = reportId };
                dbContext.Reports.Attach(entity);
                dbContext.Reports.Remove(entity);
            }

            // Save changes to SQL
            dbContext.SaveChanges();

            return new OkObjectResult(new ResponseModel
            {
                Status = true,
                Message = APIMessages.OK_MESSAGE
            });
        }

        /// <summary>
        /// Returns the list of all of the active events.
        /// </summary>
        /// <response code="200">The response was OK, and we tried to return the list of events</response>
        [HttpGet]
        [ProducesResponseType(typeof(AllEventsResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllEvents()
        {
            // Send only required info
            return new OkObjectResult(new AllEventsResponseModel
            {
                Status = true,
                Message = APIMessages.OK_MESSAGE,
                Events = (await Task.WhenAll(dbContext.Events.ToList().ConvertAll(async e => new ExportEvent
                {
                    EventId = e.Id.ToString(),
                    Latitude = e.Latitude,
                    Longitude = e.Longitude,
                    DisplayLocation = await Geocoding.ReverseGeocode(apiKey, e.Location, e.Radius),
                    Radius = e.Radius,
                    Type = e.Type
                }))).ToList()
            });
        }
    }
}