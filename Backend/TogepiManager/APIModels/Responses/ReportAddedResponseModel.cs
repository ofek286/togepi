namespace TogepiManager.APIModels.Responses {
    public class ReportAddedResponseModel : ResponseModel {
        /// <summary>
        /// The identifier of the event the report added to.
        /// (Can be a new event or an existing one)
        /// </summary>
        public string EventId { get; set; }
    }
}