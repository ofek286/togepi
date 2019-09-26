namespace TogepiManager.APIModels.Responses
{
    /// <summary>
    /// The basic response model.
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// The status of the request.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// The error message.
        /// </summary>
        public string Message { get; set; }
    }
}