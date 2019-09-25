namespace IncidentLibrary
{
    public enum ThreatLevel
    {
        PRANK,
        INJURY,
        HEALTH_CRISIS,
        CONSTRUCTION_FAULT,
        FIRE,
        WILDFIRE,
        POWER_OUTAGE,
        INFRASTRUCTURE_FAULT,
        EARTHQUAKE,
        TSUNAMI,
        STORM,
        EXTREME_WEATHER,
        TERRORISM,
        NUCLEAR_THREAT
    }

    public static class ThreatLevelMethods
    {
        public static double MaxDistanceToMerge(this ThreatLevel level) {
            switch (level) {
                // TODO: Think of distances (in meters)
                default:
                    return 10;
            }
        } 
    }
}
