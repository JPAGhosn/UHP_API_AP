using System.Collections.Generic;

namespace UHP.WebApi.ActionFilters.Models
{
    public class ExceptionResponse
    {
        public ExceptionResponse()
        {
            Message = new List<string>();
            Error = new List<string>();
        }
        public int Code { get; set; }
        public List<string> Message { get; set; }
        public List<string> Error { get; set; }
        public string StackTrace { get; set; }
    }
}