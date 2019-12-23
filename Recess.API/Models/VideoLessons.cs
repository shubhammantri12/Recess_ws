using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class VideoLessons
    {
            public int videoid { get; set; }
            public string videotitle { get; set; }
            public string videodescription { get; set; }
            public DateTime submittedOn { get; set; }
            public string submittedBy { get; set; }
            public int TeacherId { get; set; }
            public string VideoUrl { get; set; }
            public double videoRating { get; set; }
            public int videoRatingCount { get; set; }   
    }
}
