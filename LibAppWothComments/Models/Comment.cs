using System;

namespace LibApp.Models
{
    public class Comment
    {
        public string Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public bool IsLike { get; set; }
        public string Content { get; set; }
        public DateTime Added { get; set; }

    }
}
