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

namespace TogepiManager.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase {
        private TogepiContext dbContext;
        private ILogger<EventsController> logger;

        public EventsController(TogepiContext context, ILogger<EventsController> loggerArg) {
            dbContext = context;
            logger = loggerArg;

            dbContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Returns the list of all of the active events.
        /// </summary>
        /// <response code="200">The response was OK, and we tried to return the list of events</response>
        [HttpGet]
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
                            Latitude = e.Latitude,
                            Longitude = e.Longitude,
                            DisplayLocation = await Geocoding.ReverseGeocode(api, e.Location, e.Radius),
                            Radius = e.Radius,
                            Type = e.Type
                    }))).ToList()
            });
        }
    }
}