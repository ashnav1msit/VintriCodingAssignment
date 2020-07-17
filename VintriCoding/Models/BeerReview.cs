using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VintriCodingAssignment.Models
{
    public class BeerReview
    {
        [Required]
        public int id { get; set; }
        public String Username { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        public String Comments { get; set; }
    }
}