using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class SaveVideoDetails
    {
        public string title { get; set; }
        public string description { get; set; }
        public string submittedBy { get; set; }
        public int teacherId { get; set; }
        public string videoUrl { get; set; }
        public string category { get; set; }
        public string imageUrl { get; set; }
    }
}