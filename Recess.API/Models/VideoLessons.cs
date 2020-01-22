using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class VideoLessons
    {
            public int id { get; set; }
            public string title { get; set; }
            public DateTime submittedOn { get; set; }
            public string submittedBy { get; set; }
            public string imageUrl { get; set; }
            public double rating { get; set; }
            public int ratingCount { get; set; }
            public string category { get; set; }
    }
}
