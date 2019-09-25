using System.Collections.Generic;
using TogepiManager.APIModels.SubModels;

namespace TogepiManager.APIModels.Responses {
    public class AllEventsResponseModel : ResponseModel {
        public List<ExportEvent> Events { get; set; }
    }
}