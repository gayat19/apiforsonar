﻿namespace FirstAPI.Models.DTOs
{
    public class ErrorObject
    {
        public int ErrorNumber { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
