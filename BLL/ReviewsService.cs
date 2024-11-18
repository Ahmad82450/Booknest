using BLL.Models;
using CL.DALInterface;
using System.Net;
using AutoMapper;
using CL.DTO;
namespace BLL
{
    public class ReviewsService
    {
        private readonly IReviewRepository _reviewsDAL;
        private readonly IMapper _mapper;
        public ReviewsService(IReviewRepository reviewsDAL, IMapper mapper)
        {
            _reviewsDAL = reviewsDAL;
            _mapper = mapper;
        }

        public string InsertReview(Review review)
        {
            // Map the BLL Review model to the DAL ReviewDTO model
            var reviewDTO = _mapper.Map<ReviewDTO>(review);

            // Call the DAL method with the mapped DTO object
            var (isSuccess, message) = _reviewsDAL.InsertReview(reviewDTO);

            // Return a message based on the result of the DAL operation
            return isSuccess ? "Review submitted successfully." : $"Failed to submit review";
        }

        public List<Review> GetBookReviewsService(int bookID)
        {
            var (isSuccess, ReviewsDAL) = _reviewsDAL.GetBookReview(bookID);
            var reviews = _mapper.Map<List<Review>>(ReviewsDAL);

            if (isSuccess) 
            {
                return reviews;
            }
            else
            {
                return new List<Review>();
            }
        }
    }
}
