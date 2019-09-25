namespace TogepiManager.DbManagement
{
    public enum EventType
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

    public static class EventTypeMethods
    {
        public static double MaxDistanceToMerge(this EventType level) {
            switch (level) {
                // TODO: Think of distances (in meters)
                default:
                    return 10;
            }
        } 
    }
}
