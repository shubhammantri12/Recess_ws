using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class GlobalSearch
    {
        public List<TeacherDetails> teachers { get; set; }
        public List<AllCourses> courses { get; set; }
        public List<VideoLessons> videos { get; set; }
    }
}
