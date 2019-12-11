using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class RegisterClass
    {
        public int courseid { get; set; }
        public int classid { get; set; }
        public int teacherid { get; set; }
        public string username { get; set; }
        public string useremail { get; set; }
        public string classLink { get; set; }
        public string classTitle { get; set; }
    }
}