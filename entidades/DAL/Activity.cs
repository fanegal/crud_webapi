using System;
using System.Collections.Generic;

#nullable disable

namespace entidades
{
    public partial class Activity
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public DateTime Schedule { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Status { get; set; }

        public virtual Property Property { get; set; }
    }
}
