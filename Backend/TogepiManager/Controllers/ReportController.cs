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
    public class ReportController : ControllerBase {
        private TogepiContext dbContext;
        private ILogger<ReportController> logger;
        private HEREApp apiKey;

        public ReportController(TogepiContext context, ILogger<ReportController> loggerArg) {
            dbContext = context;
            logger = loggerArg;

            dbContext.Database.EnsureCreated();

            apiKey = new HEREApp {
                AppId = "nwVKikGG0miA826GlXkr",
                AppCode = "U_lOt-47GHaEnnNs34gJ6w"
            };
        }

        /// <summary>
        /// Report an event.
        /// </summary>
        /// <param name="model">The info about the event</param>
        /// <response code="200">The request was OK, and we tried to add the event</response>
        /// <response code="400">Wrong arguments were given</response>
        [HttpPost]
        [ProducesResponseType(typeof(ReportAddedResponseModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ReportAddedResponseModel), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendReport([FromBody, Required] SendReportRequestModel model) {
            if (model == null) {
                return new BadRequestObjectResult(new ReportAddedResponseModel {
                    Status = false,
                        Message = APIMessages.NO_ARGUMENTS_MESSAGE,
                        EventId = null
                });
            }

            var geoLoc = await Geocoding.Geocode(apiKey, model.Event.LocationString);

            var merged = false;
            var eventId = Guid.NewGuid();
            var newEvent = new Event {
                Latitude = geoLoc.Latitude,
                Longitude = geoLoc.Longitude,
                Radius = 10,
                Type = model.Event.Type
            };

            foreach (Event e in dbContext.Events) {
                var canMerge = newEvent.TryMerge(e, out Event mergeOutput);
                if (canMerge) {
                    dbContext.Events.Remove(e);
                    eventId = mergeOutput.Id;
                    dbContext.Events.Add(mergeOutput);
                    merged = true;
                    break;
                }
            }
            if (!merged) {
                dbContext.Events.Add(newEvent);
            }
            foreach (var report in model.Reports.Split(new string[] { "$\n" }, StringSplitOptions.RemoveEmptyEntries)) {
                var realReport = new Report {
                    Id = Guid.NewGuid(),
                    EventId = eventId,
                    Type = ReportType.TEXT,
                    Content = report,
                    UserId = model.UserId
                };
                dbContext.Reports.Add(realReport);
            }

            dbContext.SaveChanges();

            return new OkObjectResult(new ReportAddedResponseModel {
                Status = true,
                    Message = APIMessages.OK_MESSAGE,
                    EventId = eventId.ToString()
            });
        }
    }
}