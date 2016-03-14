using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace final1.Models
{
    public class Event
    {
        public int EventId { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public DateTime time { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string EditTime { get; set; }
        public string Editor { get; set; }
    }
}