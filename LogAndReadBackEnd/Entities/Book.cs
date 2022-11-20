using System;

namespace LogAndReadBackEnd.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public DateTime ReleaseTime { get; set; }
    }
}
