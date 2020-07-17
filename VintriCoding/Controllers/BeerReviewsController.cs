using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VintriCodingAssignment.DTOs;
using VintriCodingAssignment.Helper;
using VintriCodingAssignment.Models;

namespace VintriCodingAssignment.Controllers
{
    public class BeerReviewsController : ApiController
    {
        public BeerReviewsController()
        {
            APIHelper.Initialize();
        }

        [HttpPost]
        [Route("addRating")]
        public async Task<IHttpActionResult> AddRating(int id, ReviewDTO reviewDTO)
        {
            if(!ModelState.IsValid)
            {
             var message = string.Join(" | ", ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage));               
            return Json(new { message });
            }

            Boolean isIdValid = await APIHelper.ValidateID(id);
            if (!isIdValid)
            {
                return BadRequest("No beer exists with id '" + id+"'");
            }

            BeerReview review = new BeerReview
            {
                id = id,
                Username = reviewDTO.Username,
                Rating = reviewDTO.Rating,
                Comments = reviewDTO.Comments
            };

            DatabaseReadWrite.AddReview(review);

            return Ok("Review Added to database");
        }

        [HttpGet]
        [Route("getReviewsByBeerName")]
        public async Task<IHttpActionResult> GetReviewsByBeerName(String name)
        {
            if(String.IsNullOrEmpty(name))
            {
                return BadRequest("Input parameter name is null or empty");
            }
            var beers = await APIHelper.GetBeerByName(name);
            var reviews = DatabaseReadWrite.FetchAllReviews();

            var beerReviews = from b in beers
                              join r in reviews
                              on b.id equals r.id into br
                              select new
                              {
                                  Id = b.id,
                                  Name = b.name,
                                  Description = b.description,
                                  UserRatings = ConvertToReviews(br)

                              };

            return Ok(beerReviews);
        }

        private IEnumerable<ReviewDTO> ConvertToReviews(IEnumerable<BeerReview> beerReviews )
        {
            List<ReviewDTO> ReturnReviews = new List<ReviewDTO>();

            foreach (var beerReview in beerReviews)
            {
                ReturnReviews.Add( new ReviewDTO
                {
                    Comments = beerReview.Comments,
                    Username = beerReview.Username,
                    Rating = beerReview.Rating
                });

                
            }

            return ReturnReviews;

        }


    }
   
}
