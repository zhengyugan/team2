namespace webapi.Models
{
    public class ApiResponseWrapper
    {
        public string? Message { get; set; }
        public Array? Data { get; set; }

        public ApiResponseWrapper(string message, Array data)
        {
            this.Message = message;
            this.Data = data;
        }
    }
}
