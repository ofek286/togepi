using System.Net;
namespace TogepiManager.DbManagement {
    public enum EventType {
        PRANK,
        DROWING,
        FIRE,
        INJURED,
        MURDER,
        SICK,
        STUCK_IN_ROOM,
        TRAPPED,
        CAR_CRASH
    }

    public static class EventTypeMethods {
        public static double MaxDistanceToMerge(this EventType level) {
            switch (level) {
                // TODO: Think of distances (in meters)
                default : return 10;
            }
        }
    }
}