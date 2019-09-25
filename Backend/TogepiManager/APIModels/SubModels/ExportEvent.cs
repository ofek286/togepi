using TogepiManager.DbManagement;

namespace TogepiManager.APIModels.SubModels {
    public class ExportEvent {
        /// <summary>
        /// The identifier of the event.
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// The event location's latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// The event location's longitude.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// The location to display.
        /// </summary>
        public string DisplayLocation { get; set; }

        /// <summary>
        /// The event's impact radius.
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// The type of the event.
        /// </summary>
        public EventType Type { get; set; }
    }
}