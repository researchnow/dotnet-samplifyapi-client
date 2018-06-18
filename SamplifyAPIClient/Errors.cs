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

    public class SamplifyAuthenticationException : Exception
    {
        private const string _defaultMessage = "Authentication attempt failed.";
        public SamplifyAuthenticationException()
            : base(_defaultMessage)
        {
        }

        public SamplifyAuthenticationException(string message)
            : base(message)
        {
        }

        public SamplifyAuthenticationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
