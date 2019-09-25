using TogepiManager.DbManagement;

namespace TogepiManager.APIModels.SubModels {
    public class ExportEvent {
        public string EventId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string DisplayLocation { get; set; }

        public double Radius { get; set; }

        public EventType Type { get; set; }
    }
}