using System;

namespace MyRDP.Models
{
    public class Credential
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}