using System.Collections.Generic;
using TogepiManager.APIModels.SubModels;

namespace TogepiManager.APIModels.Responses
{
    /// <summary>
    /// A model to use when returning the list of all events.
    /// </summary>
    public class AllEventsResponseModel : ResponseModel
    {
        /// <summary>
        /// All active events.
        /// </summary>
        public List<ExportEvent> Events { get; set; }
    }
}