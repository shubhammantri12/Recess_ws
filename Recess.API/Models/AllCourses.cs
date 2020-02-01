using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class AllCourses
    {
        public int id { get; set; }
        public string category { get; set; }
        public string title { get; set; }
        public string submittedBy { get; set; }
        public string description { get; set; }
        public double rating { get; set; }
        public int ratingCount { get; set; }
        public string imageUrl { get; set; }
        public int totalCount { get; set; }
    }
}