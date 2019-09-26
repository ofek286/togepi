using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GeoCoordinatePortable;
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
    /// The controller that is responsible for managing the events.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        /// <summary>
        /// The database to use.
        /// </summary>
        private readonly TogepiContext dbContext;

        /// <summary>
        /// The logger to use.
        /// </summary>
        private readonly ILogger<EventController> logger;

        /// <summary>
        /// The API key to use for HERE Maps.
        /// </summary>
        private readonly HEREApp apiKey;

        /// <summary>
        /// The constructor of the controller.
        /// </summary>
        /// <param name="context">The database to use</param>
        /// <param name="loggerArg">The logger to use</param>
        public EventController(TogepiContext context, ILogger<EventController> loggerArg)
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
        /// Get details about an event.
        /// </summary>
        /// <param name="eventId">The identifier of the event</param>
        /// <response code="200">The request was OK and we tried to return the event details</response>
        /// <response code="400">Wrong arguments were given</response>
        [HttpGet]
        [ProducesResponseType(typeof(EventDetailsResposeModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(EventDetailsResposeModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEventDetails([FromQuery, Required] string eventId)
        {
            if (eventId == null)
            {
                return new BadRequestObjectResult(new EventDetailsResposeModel
                {
                    Status = false,
                    Message = APIMessages.NO_ARGUMENTS_MESSAGE
                });
            }

            // Find the event
            var ev = dbContext.Events.FirstOrDefault(e => e.Id.ToString().Equals(eventId));
            if (ev == null)
            {
                return new OkObjectResult(new EventDetailsResposeModel
                {
                    Status = false,
                    Message = APIMessages.EVENT_NOT_FOUND
                });
            }

            // Get the location's description
            var dispLoc = await Geocoding.ReverseGeocode(apiKey, ev.Location, ev.Radius);

            // Helper variables
            var reportsToReturn = new List<ExportReport>();

            // Find the related reports
            var reports = dbContext.Reports.Where(r => r.EventId == ev.Id);
            foreach (Report rep in reports)
            {
                reportsToReturn.Add(new ExportReport
                {
                    Type = rep.Type,
                    Content = rep.Content,
                    UserId = rep.UserId,
                    TimeReceived = rep.TimeReceived
                });
            }
            reportsToReturn = reportsToReturn.OrderBy(r => r.TimeReceived).ToList();

            // Return the event and its reports
            return new OkObjectResult(new EventDetailsResposeModel
            {
                Status = true,
                Message = APIMessages.OK_MESSAGE,
                EventDetails = new ExportEvent
                {
                    EventId = ev.Id.ToString(),
                    Latitude = ev.Latitude,
                    Longitude = ev.Longitude,
                    DisplayLocation = dispLoc,
                    Radius = ev.Radius,
                    Type = ev.Type
                },
                Reports = reportsToReturn
            });
        }

        /// <summary>
        /// Add an event without reports.
        /// </summary>
        /// <param name="model">The info about the event</param>
        /// <response code="200">The request was OK, and we tried to add the event</response>
        /// <response code="400">Wrong arguments were given</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddNewEvent([FromBody, Required] CreateEventRequestModel model)
        {
            if (model == null)
            {
                return new BadRequestObjectResult(new ResponseModel
                {
                    Status = false,
                    Message = APIMessages.NO_ARGUMENTS_MESSAGE
                });
            }

            // Get the location's description
            var geoLoc = await Geocoding.Geocode(apiKey, model.LocationString);

            // Add the new event
            dbContext.Events.Add(new Event
            {
                Id = Guid.NewGuid(),
                Location = geoLoc,
                Radius = 10,
                Type = model.Type
            });

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
        [HttpOptions]
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