namespace Demo_Aayush.DTOs
{
    public class DTO_Response<Request>
    {
        public required string Status { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; } 
        public Request? Data { get; set; }
    }
}
