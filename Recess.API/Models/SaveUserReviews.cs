using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class SaveUserReviews
    {
        public string username { get; set; }
        public string reviewText { get; set; }
        public string useremail { get; set; }
        public string reviewFor { get; set; }
        public DateTime submittedOn { get; set; }
        public int courseid { get; set; }
        public int videoid { get; set; }
        public int teacherid { get; set; }
    }
}