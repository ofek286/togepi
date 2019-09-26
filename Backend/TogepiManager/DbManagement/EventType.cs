namespace TogepiManager.DbManagement
{
    /// <summary>
    /// The type of event.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// A prank call, or other non-critical call.
        /// </summary>
        PRANK,

        /// <summary>
        /// Person drowning.
        /// </summary>
        DROWNING,

        /// <summary>
        /// There's a fire.
        /// </summary>
        FIRE,

        /// <summary>
        /// Someone is injured.
        /// </summary>
        INJURED,

        /// <summary>
        /// Someone is being murdered.
        /// </summary>
        MURDER,

        /// <summary>
        /// A person is not feeling well, and need medical assistant.
        /// </summary>
        SICK,

        /// <summary>
        /// A person is stuck in a close space.
        /// </summary>
        STUCK_IN_ROOM,

        /// <summary>
        /// A person is trapped, under something, or in something (not a closed space).
        /// </summary>
        TRAPPED,

        /// <summary>
        /// There was a car crash.
        /// </summary>
        CAR_CRASH
    }

    /// <summary>
    /// Helper methods for an event type.
    /// </summary>
    public static class EventTypeMethods
    {
        /// <summary>
        /// Returning the max distance possible to merge events of a type.
        /// </summary>
        /// <param name="type">The event type</param>
        /// <returns>The distance in meters</returns>
        public static double MaxDistanceToMerge(this EventType type)
        {
            /*
             * Depends on the type of event.
             * For example, two persons probably won't report the same person being sick from different places,
             * but, they might report the same fire with a great distance between them.
             */
            switch (type)
            {
                // TODO: Think of distances (in meters)
                default: return 10;
            }
        }
    }
}