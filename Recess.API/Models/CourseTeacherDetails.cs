using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class CourseTeacherDetails
    {
            public int teacherId { get; set; }
            public string teacherName { get; set; }
            public string description { get; set; }
            public string photoUrl { get; set; }
            public double teacherRating { get; set; }
            public int teacherRatingCount { get; set; }
            public string zoomid { get; set; }

    }
}