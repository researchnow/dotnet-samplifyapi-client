using System;

namespace ResearchNow.SamplifyAPIClient
{
    public class ErrorResponse
    {
        public DateTime Timestamp { get; set; }
        public string RequestID { get; set; }
        public string Path { get; set; }
        public int HTTPCode { get; set; }
        public string HTTPPhrase { get; set; }
    }
}
