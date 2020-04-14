using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class teacherContent
    {
        public teacherInfo teacherInfo { get; set; }
        public List<teacherVideoContent> Videos { get; set; }
        public List<teacherCourseContent> Courses { get; set; }
        public teacherStatistics teacherStatistics { get; set; }
    }
        public class teacherInfo
    {
        public int teacherId { get; set; }
        public string teacherName { get; set; }
        public double rating { get; set; }
        public int ratingCount { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public string emailId { get; set; }
        public int videoCount { get; set; }
        public int classCount { get; set; }
        public int courseCount { get; set; }
    }
    public class teacherVideoContent
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime submittedOn { get; set; }
        //public string description { get; set; }
        public string imageUrl { get; set; }
        public string category { get; set; }
        public string submittedBy { get; set; }
        public double rating { get; set; }
        public int ratingCount { get; set; }
    }
    public class teacherCourseContent
    {
        public int id { get; set; }
        //public string category { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        //public DateTime beginDate { get; set; }
        //public DateTime endDate { get; set; }
        public string imageUrl { get; set; }
        public string submittedBy { get; set; }
        public double rating { get; set; }
        public int ratingCount { get; set; }
    }
    public class teacherStatistics
    {
        
        public int videoCount { get; set; }
        public int classCount { get; set; }
        public int courseCount { get; set; }

    }
}