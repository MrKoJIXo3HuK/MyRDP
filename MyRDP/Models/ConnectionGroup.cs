using System;

namespace MyRDP.Models
{
    public class ConnectionGroup
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}