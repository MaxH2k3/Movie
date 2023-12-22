using System.Net;

namespace Movies.Business.globals
{
    public class ResponseDTO
    {
        public HttpStatusCode? Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public ResponseDTO()
        {
        }
        public ResponseDTO(HttpStatusCode? status, string? message, object? data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public ResponseDTO(HttpStatusCode? status, string? message)
        {
            Status = status;
            Message = message;
        }
    }
}
