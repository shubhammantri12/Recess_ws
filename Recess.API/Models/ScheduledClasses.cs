using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class ScheduledClasses
    {
        public int classId { get; set; }
        public int courseId { get; set; }
        public int teacherid { get; set; }
        public string classDescription { get; set; }
        public string classTitle { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime endDate { get; set; }
        public string teacherName { get; set; }

    }
}