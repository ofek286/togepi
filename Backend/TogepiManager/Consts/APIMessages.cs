namespace TogepiManager.Consts
{
    /// <summary>
    /// Collection of API error messages.
    /// </summary>
    public class APIMessages
    {
        /// <summary>
        /// Message to send when the request succeeded.
        /// </summary>
        public const string OK_MESSAGE = "OK";

        /// <summary>
        /// Message to send when the arguments given were incompatible.
        /// </summary>
        public const string NO_ARGUMENTS_MESSAGE = "Bad arguments given";

        /// <summary>
        /// Message to send when requesting an event that doesn't exist.
        /// </summary>
        public const string EVENT_NOT_FOUND = "Event was not found.";
    }
}