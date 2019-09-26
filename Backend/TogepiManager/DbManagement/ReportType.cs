namespace TogepiManager.DbManagement
{
    /// <summary>
    /// The type of report that was sent.
    /// 0 - Text
    /// 1 - Photo
    /// 2 - Voice
    /// </summary>
    public enum ReportType
    {
        /// <summary>
        /// Text report
        /// </summary>
        TEXT,

        /// <summary>
        /// Image report
        /// </summary>
        PHOTO,

        /// <summary>
        /// Voice report
        /// </summary>
        VOICE
    }
}