using System.Collections.Generic;
using TogepiManager.APIModels.SubModels;

namespace TogepiManager.APIModels.Requests
{
    /// <summary>
    /// A model to use when sending a new report.
    /// </summary>
    public class SendReportRequestModel
    {
        /// <summary>
        /// The details about the event.
        /// </summary>
        public CreateEventRequestModel Event { get; set; }

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