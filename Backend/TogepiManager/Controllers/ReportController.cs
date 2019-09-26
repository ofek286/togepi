using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using HEREMaps.Base;
using HEREMaps.LocationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TogepiManager.APIModels.Requests;
using TogepiManager.APIModels.Responses;
using TogepiManager.APIModels.SubModels;
using TogepiManager.Consts;
using TogepiManager.DbManagement;

namespace TogepiManager.Controllers
{
    /// <summary>
    /// The controller that is responsible to manage the incoming reports.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        /// <summary>
        /// The database to use.
        /// </summary>
        private readonly TogepiContext dbContext;

        /// <summary>
        /// The logger to use.
        /// </summary>
        private readonly ILogger<ReportController> logger;

        /// <summary>
        /// The API key to use for HERE Maps.
        /// </summary>
        private readonly HEREApp apiKey;

        /// <summary>
        /// The constructor of the controller.
        /// </summary>
        /// <param name="context">The database to use</param>
        /// <param name="loggerArg">The logger to use</param>
        public ReportController(TogepiContext context, ILogger<ReportController> loggerArg)
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
        /// Report an event.
        /// </summary>
        /// <param name="model">The info about the event</param>
        /// <response code="200">The request was OK, and we tried to add the event</response>
        /// <response code="400">Wrong arguments were given</response>
        [HttpPost]
        [ProducesResponseType(typeof(ReportAddedResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ReportAddedResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendReport([FromBody, Required] SendReportRequestModel model)
        {
            if (model == null)
            {
                return new BadRequestObjectResult(new ReportAddedResponseModel
                {
                    Status = false,
                    Message = APIMessages.NO_ARGUMENTS_MESSAGE,
                    EventId = null
                });
            }

            try
            {
                // Get the geolocation
                var geoLoc = await Geocoding.Geocode(apiKey, model.Event.LocationString);

                // Helper variables
                var merged = false;
                var eventId = Guid.NewGuid();

                // The new event
                var newEvent = new Event
                {
                    Id = eventId,
                    Latitude = geoLoc.Latitude,
                    Longitude = geoLoc.Longitude,
                    Radius = 10,
                    Type = model.Event.Type
                };

                // Checking merge options
                foreach (Event e in dbContext.Events)
                {
                    var canMerge = newEvent.TryMerge(e, out Event mergeOutput);
                    if (canMerge)
                    {
                        // Replace the id on existing reports
                        foreach (Report r in dbContext.Reports.Where(r => r.EventId == e.Id))
                        {
                            r.EventId = mergeOutput.Id;
                        }
                        // Remove the existing event
                        dbContext.Events.Remove(e);

                        // Saving the new event id
                        eventId = mergeOutput.Id;

                        // Adding the merged event
                        dbContext.Events.Add(mergeOutput);
                        merged = true;
                        break;
                    }
                }
                // Adding the new event if it wasn't merged
                if (!merged)
                {
                    dbContext.Events.Add(newEvent);
                }

                // Add the reports
                foreach (var report in model.Reports.Split(new string[] { "\n$" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // Image reports
                    if (report.StartsWith("&&"))
                    {
                        // Extract the Base64 images
                        var images = report.Substring(2).Split(new string[] { "\n&&" }, StringSplitOptions.RemoveEmptyEntries);
                        
                        // Adding each of the image reports
                        foreach (var image in images)
                        {
                            var imageReport = new Report
                            {
                                Id = Guid.NewGuid(),
                                EventId = eventId,
                                Type = ReportType.PHOTO,
                                Content = image,
                                UserId = model.UserId,
                                TimeReceived = DateTime.Now
                            };
                            dbContext.Reports.Add(imageReport);
                        }
                        continue;
                    }

                    // Adding the text reports
                    var realReport = new Report
                    {
                        Id = Guid.NewGuid(),
                        EventId = eventId,
                        Type = ReportType.TEXT,
                        Content = report,
                        UserId = model.UserId,
                        TimeReceived = DateTime.Now
                    };
                    dbContext.Reports.Add(realReport);
                }

                // Saving changes
                dbContext.SaveChanges();

                // Sending the event id
                return new OkObjectResult(new ReportAddedResponseModel
                {
                    Status = true,
                    Message = APIMessages.OK_MESSAGE,
                    EventId = eventId.ToString()
                });

            }
            catch (Exception ex)
            {
                return new OkObjectResult(new ReportAddedResponseModel
                {
                    Status = false,
                    Message = ex.Message,
                    EventId = null
                });
            }
        }
    }
}