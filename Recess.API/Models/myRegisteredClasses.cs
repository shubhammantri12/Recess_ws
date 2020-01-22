using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recess.API.Models
{
    public class myRegisteredClasses
    {
        public int classId { get; set; }

        public int courseId { get; set; }

        public string classTitle { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime endDate { get; set; }
    }
}