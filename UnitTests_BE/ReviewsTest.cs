using AutoMapper;
using BLL;
using BLL.Models;
using BooknestAPI.Models;
using CL.DALInterface;
using CL.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooknestAPI.Tests
{
    [TestClass]
    public class ReviewsTest
    {
        [TestMethod]
        public void Insert_review_Success()
        {
            // Arrange
            var mockUserDAL = new Mock<IReviewRepository>();
            var mockMapper = new Mock<IMapper>();

            //User Input
            var review = new Reviews
            {
                reviewText = "test",
                bookID = 1,
                userID = 1,
            };

            //Service Data
            var reviewModel = new Review
            {
                reviewText = review.reviewText,
                bookID = review.bookID,
                userID = review.userID,
            };

            //Database inserted data
            var reviewDTO = new ReviewDTO
            {
                reviewText = reviewModel.reviewText,
                bookID = reviewModel.bookID,
                userID = reviewModel.userID,
            };

            mockUserDAL.Setup(dal => dal.InsertReview(It.IsAny<ReviewDTO>())).Returns((true, "Thanks for your review!!"));
            mockMapper.Setup(mapper => mapper.Map<ReviewDTO>(reviewModel)).Returns(reviewDTO);

            var reviewsService = new ReviewsService(mockUserDAL.Object, mockMapper.Object);

            var result = reviewsService.InsertReview(reviewModel);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Review submitted successfully.", result);
        }
    }
}
