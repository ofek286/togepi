using TogepiManager.DbManagement;

namespace TogepiManager.APIModels.Requests {
    /// <summary>
    /// A model to use when creating a new event.
    /// </summary>
    public class CreateEventRequestModel {

        /// <summary>
        /// The location's textual description.
        /// </summary>
        public string LocationString { get; set; }

        /// <summary>
        /// The type of the event.
        /// </summary>
        public EventType Type { get; set; }
    }
}