using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class ViewAllDetails
    {
        public List<TeacherDetails> teachers { get; set; }
        public List<AllCourses> courses { get; set; }
        public List<VideoLessons> videos { get; set; }
    }
    public class viewAll
    {
        public string teacherName { get; set; }
        public int id { get; set; }
        public string description { get; set; }
        public double rating { get; set; }
        public int ratingCount { get; set; }
        public string imageUrl { get; set; }
        public int totalCount { get; set; }
        public string title { get; set; }
        public string category { get; set; }
        public DateTime submittedOn { get; set; }
        public string submittedBy { get; set; }
    }
    public class seeAllDetails
    {
        public string type { get; set; }
        public int totalCount { get; set; }
        public List<viewAll> listData { get; set; }
    }
}