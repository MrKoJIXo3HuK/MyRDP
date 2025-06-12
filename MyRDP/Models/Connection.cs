using System;

namespace MyRDP.Models
{
    public class Connection
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Address { get; set; }
        public string Gateway { get; set; }
        public Guid CredentialId { get; set; }
        public Guid GroupId { get; set; }
    }
}