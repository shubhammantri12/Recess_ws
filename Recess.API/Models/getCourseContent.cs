using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class getCourseContent
    {
        public courseDetailsByCourseid coursecontent {get;set;}
        //public int courseid { get; set; }
        //public string courseCategory { get; set; }
        //public string title { get; set; }
        //public string description { get; set; }
        //public DateTime beginDate { get; set; }
        //public DateTime endDate { get; set; }
        //public string submittedBy { get; set; }
        //public string imageUrl { get; set; }
        //public string VideoUrl { get; set; }
        //public double courseRating { get; set; }
        //public int totalRatingCount { get; set; }
        public CourseTeacherDetails teachers { get; set; }
        public List<ScheduledClasses> scheduledClasses { get; set; }
        public List<AllCourses> similarCourses { get; set; }

    }
    public class courseDetailsByCourseid
    {
        public int courseid { get; set; }
        public string courseCategory { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime endDate { get; set; }
        public string submittedBy { get; set; }
        public string imageUrl { get; set; }
        public string VideoUrl { get; set; }
        public double courseRating { get; set; }
        public int totalRatingCount { get; set; }
    }
}