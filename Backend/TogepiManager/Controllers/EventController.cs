using System;
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

namespace TogepiManager.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase {
        private TogepiContext dbContext;
        private ILogger<EventController> logger;
        private HEREApp apiKey;

        public EventController(TogepiContext context, ILogger<EventController> loggerArg) {
            dbContext = context;
            logger = loggerArg;

            dbContext.Database.EnsureCreated();

            apiKey = new HEREApp {
                AppId = "nwVKikGG0miA826GlXkr",
                AppCode = "U_lOt-47GHaEnnNs34gJ6w"
            };
        }

        /// <summary>
        /// Add an event without reports.
        /// </summary>
        /// <param name="model">The info about the event</param>
        /// <response code="200">The request was OK, and we tried to add the event</response>
        /// <response code="400">Wrong arguments were given</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddNewEvent([FromBody, Required] CreateEventRequestModel model) {
            if (model == null) {
                return new BadRequestObjectResult(new ResponseModel {
                    Status = false,
                        Message = APIMessages.NO_ARGUMENTS_MESSAGE
                });
            }

            var geoLoc = await Geocoding.Geocode(apiKey, model.LocationString);
            dbContext.Events.Add(new Event {
                Id = Guid.NewGuid(),
                    Latitude = geoLoc.Latitude,
                    Longitude = geoLoc.Longitude,
                    Radius = 10,
                    Type = model.Type
            });
            dbContext.SaveChanges();
            return new OkObjectResult(new ResponseModel {
                Status = true,
                    Message = APIMessages.OK_MESSAGE
            });
        }

        /// <summary>
        /// Returns the list of all of the active events.
        /// </summary>
        /// <response code="200">The response was OK, and we tried to return the list of events</response>
        [HttpOptions]
        [ProducesResponseType(typeof(AllEventsResponseModel), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllEvents() {
            return new OkObjectResult(new AllEventsResponseModel {
                Status = true,
                    Message = APIMessages.OK_MESSAGE,
                    Events = (await Task.WhenAll(dbContext.Events.ToList().ConvertAll(async e => new ExportEvent {
                        EventId = e.Id.ToString(),
                            Latitude = e.Location.Latitude,
                            Longitude = e.Location.Longitude,
                            DisplayLocation = await Geocoding.ReverseGeocode(apiKey, e.Location, e.Radius),
                            Radius = e.Radius,
                            Type = e.Type
                    }))).ToList()
            });
        }
    }
}