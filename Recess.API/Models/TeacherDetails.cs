using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class TeacherDetails
    {
        public string name { get; set; }
        public int id { get; set; }
        public string description { get; set; }
        public double rating { get; set; }
        public int ratingCount { get; set; }
        public string imageUrl { get; set; }
        
       
    }
    public class SaveTeacherDetails
    {
        public string name { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string photourl { get; set; }

        public string courseCategory { get; set; }
        public string Gender { get; set; }
    }
}