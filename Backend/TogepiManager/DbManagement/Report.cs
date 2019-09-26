using System;

namespace TogepiManager.DbManagement
{
    /// <summary>
    /// The model of a report.
    /// </summary>
    public class Report
    {
        /// <summary>
        /// The identifier of the report.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The identifier of the related event.
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// The type of report.
        /// </summary>
        public ReportType Type { get; set; }

        /// <summary>
        /// The content of the report.
        /// If image or voice, the content is Base64 of the binary content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The user identifer of the reporter.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The timestamp of the report.
        /// </summary>
        public DateTime TimeReceived { get; set; }
    }
}