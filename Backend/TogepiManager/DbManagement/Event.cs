using System;
using GeoCoordinatePortable;
using IncidentLibrary;

namespace TogepiManager.DbManagement
{
    /// <summary>
    /// The model of an event.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// The identifier of the event.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The latitude coordinate.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// The longitude coordinate.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// The impact radius.
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// The type of event.
        /// </summary>
        public EventType Type { get; set; }

        /// <summary>
        /// The location as a GeoCoordinate.
        /// </summary>
        public GeoCoordinate Location
        {
            get
            {
                return new GeoCoordinate(Latitude, Longitude);
            }
            set
            {
                Latitude = value.Latitude;
                Longitude = value.Longitude;
            }
        }

        /// <summary>
        /// Try to merge two events.
        /// </summary>
        /// <param name="otherThreat">The other event</param>
        /// <param name="mergedThreat">The merged event, if mergable</param>
        /// <returns>Are the threats mergable?</returns>
        public bool TryMerge(Event otherThreat, out Event mergedThreat)
        {
            // TODO: Add complex logic here (Future feature)

            // Check if the same event type
            if (Type != otherThreat.Type)
            {
                mergedThreat = null;
                return false;
            }

            // Get the maximum distance between events of that type.
            var maxDist = Type.MaxDistanceToMerge();
            if (maxDist < Location.GetDistanceTo(otherThreat.Location))
            {
                mergedThreat = null;
                return false;
            }

            // Return the merged event
            mergedThreat = new Event
            {
                Id = Guid.NewGuid(),
                Location = Location.MidPoint(otherThreat.Location),
                Type = Type,
                Radius = otherThreat.Radius
            };
            return true;
        }
    }
}