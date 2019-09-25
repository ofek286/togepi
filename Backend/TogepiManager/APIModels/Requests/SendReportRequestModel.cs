using System.Collections.Generic;
using TogepiManager.APIModels.SubModels;

namespace TogepiManager.APIModels.Requests {
    public class SendReportRequestModel {
        /// <summary>
        /// The details about the event.
        /// </summary>
        public CreateEventRequestModel Event { get; set; }

        // /// <summary>
        // /// The history of reports about this event. (For future continuation)
        // /// </summary>
        // public List<ImportReport> Reports { get; set; }

        /// <summary>
        /// The reports the user sent (Separated by "\n$").
        /// </summary>
        public string Reports { get; set; }

        /// <summary>
        /// The user identifier of the reporter.
        /// </summary>
        public string UserId { get; set; }
    }
}