﻿namespace Notes.API.DTO
{
    public class AddNoteRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ColorHex { get; set; }
    }
}
