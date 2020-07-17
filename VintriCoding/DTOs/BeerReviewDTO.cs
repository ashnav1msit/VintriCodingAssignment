using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VintriCodingAssignment.DTOs
{
    public class BeerReviewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ReviewDTO> userRatings { get; set; }
    }
}