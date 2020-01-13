using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class AllCourses
    {
        public int courseId { get; set; }
        public string courseCategory { get; set; }
        public string title { get; set; }
        public string submittedBy { get; set; }
        public double teacherRating { get; set; }
        public int ratingCount { get; set; }
        public string imageUrl { get; set; }
    }
}