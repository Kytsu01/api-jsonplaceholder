﻿namespace JsonPlaceholderImporter.Dtos
{
    public class TodoCreateDto
    {
        public int UserId { get; set; }
        public string Title { get; set; } = "";
        public bool Completed { get; set; }
    }
}
