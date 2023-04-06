namespace Notes.API.Models.DTO
{
    public class UpdateNotRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ColorHex { get; set; }
    }
}
