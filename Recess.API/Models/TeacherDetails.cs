using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class TeacherDetails
    {
        public string name { get; set; }
        public int teacherId { get; set; }
        public string description { get; set; }
        public double currentRating { get; set; }
        public int totalcount { get; set; }
        public string photourl { get; set; }
       
    }
    public class TeacherRating
    {
        public string currentRating { get; set; }
        public int totalcount { get; set; }
       

    }
}