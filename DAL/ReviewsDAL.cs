using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using CL.DTO;
using CL.DALInterface;

namespace DAL
{
    public class ReviewsDAL : IReviewRepository
    {
        public readonly IDBConnection _conn;
        public ReviewsDAL(IDBConnection conn)
        {
            _conn = conn;
        }

        public (bool, string) InsertReview(ReviewDTO review)
        {
            try
            {
                _conn.Open();
                string query = "INSERT INTO `reviews` (`reviewText`, `userID`, `bookID`) VALUES (@reviewText, @userID, @bookID);";
                IDBCommandWrapper cmd = _conn.CreateCommand(query);
                cmd.ParametersAddWithValue("@reviewText", review.reviewText);
                cmd.ParametersAddWithValue("@userID", review.userID);
                cmd.ParametersAddWithValue("@bookID", review.bookID);
                cmd.ExecuteNonQuery();

                return (true, "Thanks for your review!!");
            }
            catch (MySqlException sqlEx)
            {
                // Log SQL-specific error
                Console.WriteLine($"Database error occurred: {sqlEx.Message}");
                return (false, "Database error occurred. Please try again later.");
            }
            catch (Exception ex)
            {
                // Log general error
                Console.WriteLine($"An error occurred: {ex.Message}");
                return (false, "Something went wrong. Please try again later.");
            }
            finally
            {
                _conn.Close(); // Ensure connection is closed
            }
        }

        public (bool, List<ReviewDTO>) GetBookReview(int bookID)
        {
            List<ReviewDTO> reviewDTOs = new List<ReviewDTO>();
            try
            {
                _conn.Open();
                string query = "SELECT * FROM `reviews` WHERE `bookID` = @bookID;";
                IDBCommandWrapper cmd = _conn.CreateCommand(query);
                cmd.ParametersAddWithValue("@bookID", bookID);

                using (MySqlDataReader myReader = cmd.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        ReviewDTO reviewDTO = new ReviewDTO
                        {
                            reviewID = myReader.GetInt32("reviewID"),
                            reviewText = myReader.GetString("reviewText"),
                            userID = myReader.GetInt32("userID"),
                            bookID = myReader.GetInt32("bookID"),
                        };
                        reviewDTOs.Add(reviewDTO);
                    }
                }

                return (true, reviewDTOs);
            }
            catch (MySqlException sqlEx)
            {
                // Log SQL-specific error
                Console.WriteLine($"Database error occurred: {sqlEx.Message}");
                return (false, new List<ReviewDTO>());
            }
            catch (Exception ex)
            {
                // Log general error
                Console.WriteLine($"An error occurred: {ex.Message}");
                return (false, new List<ReviewDTO>());
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
