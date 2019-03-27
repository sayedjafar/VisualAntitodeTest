using System;

namespace FunFacts.Models
{
    public class FunFact
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Deleted { get; set; }
    }
}
