using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeoCoordinatePortable;
using IncidentLibrary;

namespace TogepiManager.DbManagement {
    public class Event {
        public Guid Id { get; set; }
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Radius { get; set; }

        public EventType Type { get; set; }

        public GeoCoordinate Location {
            get {
                return new GeoCoordinate(Latitude, Longitude);
            }
            set {
                Latitude = value.Latitude;
                Longitude = value.Longitude;
            }
        }

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