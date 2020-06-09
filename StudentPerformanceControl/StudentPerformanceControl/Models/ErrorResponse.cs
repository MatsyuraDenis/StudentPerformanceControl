using System;

namespace StudentPerformanceControl.Models
{
    [Serializable]
    public class ErrorResponse
    {
        public int ErrorStatus { get; set; }
        public string Message { get; set; }

        public ErrorResponse(int errorStatus = 500, string message = "unknown")
        {
            ErrorStatus = errorStatus;
            Message = message;
        }
    }
}