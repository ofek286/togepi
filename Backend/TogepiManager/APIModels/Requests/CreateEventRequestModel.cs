using TogepiManager.DbManagement;

namespace TogepiManager.APIModels.Requests {
    public class CreateEventRequestModel {
        // /// <summary>
        // /// The latitude of the event location.
        // /// </summary>
        // public double Latitude { get; set; }

        // /// <summary>
        // /// The longitude of the event location.
        // /// </summary>
        // public double Longitude { get; set; }

        /// <summary>
        /// The location textual description.
        /// </summary>
        public string LocationString { get; set; }

        // /// <summary>
        // /// The radius of the relevant area.
        // /// </summary>
        // public double Radius { get; set; }

        /// <summary>
        /// The type of the event.
        /// </summary>
        public EventType Type { get; set; }
    }
}