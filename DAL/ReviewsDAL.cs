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

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (false, "Something wrong happened try again later");
            }

            finally { _conn.Close(); }

        }

        public (bool ,List<ReviewDTO>) GetAllReviews() 
        {
            List<ReviewDTO> reviewDTOs = new List<ReviewDTO>();
            try
            {
                _conn.Open();
                string query = "SELECT * FROM `reviews`";
                IDBCommandWrapper cmd = _conn.CreateCommand(query);
                using (MySqlDataReader myReader = cmd.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        ReviewDTO reviewDTO = new()
                        {
                            reviewText = myReader.GetString("reviewText"),
                            userID = myReader.GetInt32("userID"),
                            bookID = myReader.GetInt32("bookID"),
                        };
                        reviewDTOs.Add(reviewDTO);
                    }

                }

                return (true, reviewDTOs);
            }
            catch (Exception ex) 
            {
                return (false, new List<ReviewDTO>());
            }

            finally { _conn.Close(); }
        }
    }
}
