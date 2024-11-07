using Microsoft.AspNetCore.Mvc;
using BooknestAPI.Models;
using BLL;
using AutoMapper;
using BLL.Models;
using CL.DTO;


namespace BooknestAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class ReviewsController : Controller
    {
        private readonly ReviewsService _reviewsService;
        private readonly IMapper _mapper;
        public ReviewsController(ReviewsService reviewsService, IMapper mapper)
        {
            _reviewsService = reviewsService;
            _mapper = mapper;
        }
        // POST: api/Reviews/SubmitReview
        [HttpPost("SubmitReview")]
        public IActionResult SubmitReview([FromBody] Reviews review)
        {
            if (review == null)
            {
                return BadRequest("Review data is null.");
            }

            var ReviewModel = new Review
            {
                userID = review.userID,
                reviewText = review.reviewText,
                bookID = review.bookID,
            };
            _reviewsService.InsertReview(ReviewModel);

            Console.WriteLine(review);

            // Assuming the save is successful, return a success response
            return Ok(new { message = "Review submitted successfully." });
        }
    }
}
