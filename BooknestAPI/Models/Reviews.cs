namespace BooknestAPI.Models
{
    public class Reviews
    {
        public int reviewID { get; set; }
        public string reviewText { get; set; }
        public int bookID { get; set; }
        public int userID { get; set; }
    }
}
