using CL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL.DALInterface
{
    public interface IReviewRepository
    {
        public (bool, string) InsertReview(ReviewDTO review);
        public (bool, List<ReviewDTO>) GetAllReviews();
    }
}
