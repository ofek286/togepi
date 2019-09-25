using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TogepiManager.DbManagement {
    public class Report {
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public ReportType Type { get; set; }

        /// <summary>
        /// The content of the report.
        /// If image or voice, the content is Base64 of the binary content.
        /// </summary>
        public string Content { get; set; }

        public string UserId { get; set; }
    }
}