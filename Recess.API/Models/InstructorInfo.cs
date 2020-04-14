using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class InstructorInfo
    {
        public int teacherId { get; set; }
        public string teacherName { get; set; }
        public double rating { get; set; }
        public int ratingCount { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public string emailId { get; set; }
        public List<teacherCourseContent> courses { get; set; }
    }
}