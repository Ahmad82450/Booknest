using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Review
    {
        public int reviewID { get; set; }
        public string reviewText { get; set; }
        public int bookID { get; set; }
        public int userID { get; set; }
    }
}
