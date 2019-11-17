using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class SaveCourseDetails
    {
        public string courseCategory { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime endDate { get; set; }
        public string submittedBy { get; set; }
        public string imageUrl { get; set; }
        public string VideoUrl { get; set; }
    }
}