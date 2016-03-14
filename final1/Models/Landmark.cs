using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace final1.Models
{
    public class Landmark
    {
        public int LandmarkId { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string LandmarkName { get; set; }
        public string EditTime { get; set; }
        public string Editor { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 3)]
        public string Description { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Direction { get; set; }
    }
}