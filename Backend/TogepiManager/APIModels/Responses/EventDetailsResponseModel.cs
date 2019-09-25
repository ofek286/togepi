using System.Collections.Generic;
using TogepiManager.APIModels.SubModels;

namespace TogepiManager.APIModels.Responses {
    /// <summary>
    /// The model to use when returning information about an event.
    /// </summary>
    public class EventDetailsResposeModel : ResponseModel {
        /// <summary>
        /// The details of the event.
        /// </summary>
        public ExportEvent EventDetails { get; set; }

        /// <summary>
        /// The reports related to the event.
        /// </summary>
        public List<ExportReport> Reports { get; set; }
    }
}