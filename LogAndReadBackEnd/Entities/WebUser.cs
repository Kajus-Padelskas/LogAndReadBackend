using System;

namespace LogAndReadBackEnd.Entities
{
    public class WebUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
