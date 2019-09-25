using System;
using GeoCoordinatePortable;

namespace IncidentLibrary
{
    public class Threat
    {
        public Guid ThreatId { get; set; }
        
        public GeoCoordinate Location { get; set; }

        public double Radius { get;set; }

        public ThreatLevel Type { get; set; }

        public bool TryMerge(Threat otherThreat, out Threat mergedThreat) {
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

            mergedThreat = new Threat {
                ThreatId = Guid.NewGuid(),
                Location = Location.MidPoint(otherThreat.Location),
                Type = Type
            };
            return true;
        }
    }
}