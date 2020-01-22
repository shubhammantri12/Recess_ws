using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class videoContent
    {
        public videoInfo videoInfo { get; set; }
        //public teacherInfoForVideo teacherInfo { get; set; }
        public List<similarVideoDetails> similarVideos { get; set; }
        public List<videoCourseContent> relatedCourses { get; set; }
    }
    public class videoInfo
    {
        public string title { get; set; }
        public string description { get; set; }
        public DateTime submittedOn { get; set; }
        public string videoUrl { get; set; }
        public string category { get; set; }
        public int teacherId { get; set; }
        public string teacherName { get; set; }
        public double rating { get; set; }
        public int ratingCount { get; set; }

    }
    public class teacherInfoForVideo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double rating { get; set; }
        public int ratingCount { get; set; }
        public string photoUrl { get; set; }

    }
    public class similarVideoDetails
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime submittedOn { get; set; }
        public string imageUrl { get; set; }
        public string category { get; set; }
        public string submittedBy { get; set; }
        public double rating { get; set; }
        public int ratingCount { get; set; }
    }
    public class videoCourseContent
    {
        public int id { get; set; }
        public string category { get; set; }
        public string title { get; set; }
        //public string description { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime endDate { get; set; }
        public string imageUrl { get; set; }
        public double rating { get; set; }
        public int ratingCount { get; set; }
        public string submittedBy { get; set; }
    }
}