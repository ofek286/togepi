using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeoCoordinatePortable;
using IncidentLibrary;

namespace TogepiManager.DbManagement {
    public class Event {
        public Guid Id { get; set; }

        public GeoCoordinate Location { get; set; }

        public double Latitude { get { return Location.Latitude; } }

        public double Longitude { get { return Location.Longitude; } }

        public double Radius { get; set; }

        public EventType Type { get; set; }

        public bool TryMerge(Event otherThreat, out Event mergedThreat) {
            // TODO: Add complex logic here

            if (Type != otherThreat.Type) {
                mergedThreat = null;
                return false;
            }

            var maxDist = Type.MaxDistanceToMerge();
            if (maxDist < Location.GetDistanceTo(otherThreat.Location)) {
                mergedThreat = null;
                return false;
            }

            mergedThreat = new Event {
                Id = Guid.NewGuid(),
                Location = Location.MidPoint(otherThreat.Location),
                Type = Type
            };
            return true;
        }
    }
}