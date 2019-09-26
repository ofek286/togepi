using System;
using TogepiManager.DbManagement;

namespace TogepiManager.APIModels.SubModels
{
    /// <summary>
    /// The model to use when describing an event.
    /// </summary>
    public class ExportReport
    {
        /// <summary>
        /// The type of the report (Text, Photo or Voice).
        /// </summary>
        public ReportType Type { get; set; }

        /// <summary>
        /// The content of the report.
        /// If image or voice, the content is Base64 of the binary content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The user identifier of the reporter.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The time when the report was received.
        /// </summary>
        public DateTime TimeReceived { get; set; }
    }
}