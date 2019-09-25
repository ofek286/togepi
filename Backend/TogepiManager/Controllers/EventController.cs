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

        public EventController(TogepiContext context, ILogger<EventController> loggerArg) {
            dbContext = context;
            logger = loggerArg;

            dbContext.Database.EnsureCreated();
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel), (int) HttpStatusCode.BadRequest)]
        public IActionResult AddNewEvent([FromBody, Required] CreateEventRequestModel model) {
            if (model == null) {
                return new BadRequestObjectResult(new ResponseModel {
                    Status = false,
                        Message = APIMessages.NO_ARGUMENTS_MESSAGE
                });
            }

            dbContext.Events.Add(new Event {
                Id = Guid.NewGuid(),
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    Radius = model.Radius,
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
            var api = new HEREApp {
                AppId = "nwVKikGG0miA826GlXkr",
                AppCode = "U_lOt-47GHaEnnNs34gJ6w"
            };
            return new OkObjectResult(new AllEventsResponseModel {
                Status = true,
                    Message = APIMessages.OK_MESSAGE,
                    Events = (await Task.WhenAll(dbContext.Events.ToList().ConvertAll(async e => new ExportEvent {
                        EventId = e.Id.ToString(),
                            Latitude = e.Location.Latitude,
                            Longitude = e.Location.Longitude,
                            DisplayLocation = await Geocoding.ReverseGeocode(api, e.Location, e.Radius),
                            Radius = e.Radius,
                            Type = e.Type
                    }))).ToList()
            });
        }
    }
}