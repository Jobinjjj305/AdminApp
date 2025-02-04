﻿using System.ComponentModel.DataAnnotations;

namespace StudentApp.Models
{
    public class UserRegistration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool AcceptTerms { get; set; }
    }
}
