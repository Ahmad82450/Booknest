﻿using AutoMapper;
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

        [TestMethod]
        public void Insert_review_Fail()
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

            mockUserDAL.Setup(dal => dal.InsertReview(It.IsAny<ReviewDTO>())).Returns((false, "Something wrong happened try again later"));
            mockMapper.Setup(mapper => mapper.Map<ReviewDTO>(reviewModel)).Returns(reviewDTO);

            var reviewsService = new ReviewsService(mockUserDAL.Object, mockMapper.Object);

            var result = reviewsService.InsertReview(reviewModel);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Failed to submit review", result);
        }

        [TestMethod]
        public void GetBookReviewsService_Success()
        {
            // Arrange
            var mockReviewsDAL = new Mock<IReviewRepository>();
            var mockMapper = new Mock<IMapper>();

            // Mock data for ReviewsDAL
            var reviewsDAL = new List<ReviewDTO>
            {
                new ReviewDTO { reviewID = 1, bookID = 1, reviewText = "Great book!"},
                new ReviewDTO { reviewID = 2, bookID = 2, reviewText = "Not bad." }
            };

            var reviews = new List<Review>
            {
                new Review { reviewID = 1, bookID = 1, reviewText = "Great book!" },
                new Review { reviewID = 2, bookID = 1, reviewText = "Not bad."}
            };

            // Setup mocks
            mockReviewsDAL.Setup(dal => dal.GetBookReview(It.IsAny<int>())).Returns((true, reviewsDAL));
            mockMapper.Setup(mapper => mapper.Map<List<Review>>(reviewsDAL)).Returns(reviews);

            var bookService = new ReviewsService(mockReviewsDAL.Object, mockMapper.Object);

            // Act
            var result = bookService.GetBookReviewsService(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Great book!", result[0].reviewText);
        }

        [TestMethod]
        public void GetBookReviewsService_Failure()
        {
            // Arrange
            var mockReviewsDAL = new Mock<IReviewRepository>();
            var mockMapper = new Mock<IMapper>();

            // Mock data for ReviewsDAL (empty list in case of failure)
            var reviewsDAL = new List<ReviewDTO>();

            // Setup mocks
            mockReviewsDAL.Setup(dal => dal.GetBookReview(It.IsAny<int>())).Returns((false, reviewsDAL));
            mockMapper.Setup(mapper => mapper.Map<List<Review>>(reviewsDAL)).Returns(new List<Review>());

            var bookService = new ReviewsService(mockReviewsDAL.Object, mockMapper.Object);

            // Act
            var result = bookService.GetBookReviewsService(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);  // Since isSuccess is false, we expect an empty list.
        }

        [TestMethod]
        public void GetBookReviewsService_EmptyReviewsDAL()
        {
            // Arrange
            var mockReviewsDAL = new Mock<IReviewRepository>();
            var mockMapper = new Mock<IMapper>();

            // Mock empty ReviewsDAL
            var reviewsDAL = new List<ReviewDTO>();  // Empty list but successful retrieval

            // Setup mocks
            mockReviewsDAL.Setup(dal => dal.GetBookReview(It.IsAny<int>())).Returns((true, reviewsDAL));
            mockMapper.Setup(mapper => mapper.Map<List<Review>>(reviewsDAL)).Returns(new List<Review>());

            var bookService = new ReviewsService(mockReviewsDAL.Object, mockMapper.Object);

            // Act
            var result = bookService.GetBookReviewsService(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);  // Expect an empty list but no error
        }
    }
}
